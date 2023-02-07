using Serilog.Context;
using System.Security.Claims;

namespace WebApi.Common.Extensions.SerialogServices;

public class UserIdEnricher
{
    private readonly RequestDelegate next;

    public UserIdEnricher(RequestDelegate next)
    {
        this.next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var userClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        var userId = "Anonymous";
        if (userClaim != null) userId = userClaim.Value;

        LogContext.PushProperty("UserId", userId);

        return next(context);
    }
}