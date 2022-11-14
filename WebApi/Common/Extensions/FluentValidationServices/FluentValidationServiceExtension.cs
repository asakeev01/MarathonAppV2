using FluentValidation;
using WebApi.Endpoints.Marathons.Dtos.Requests;

namespace WebApi.Common.Extensions.FluentValidationServices;

public static class FluentValidationServiceExtension
{
    internal static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateMarathonRequestValidator>();
    }

    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("ky-KG", "NotEmptyValidator", "'{PropertyName}' толтурулушу керек.");
            AddTranslation("ky-KG", "GreaterThanValidator", "'{PropertyName}' {ComparisonValue}-ден чоңураак болушу керек.");
        }
    }
}