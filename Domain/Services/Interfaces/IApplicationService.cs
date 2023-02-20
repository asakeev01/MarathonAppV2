using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Distances;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;

namespace Domain.Services.Interfaces;

public interface IApplicationService
{
    Task<Application> CreateApplicationViaPromocode(User user, Distance distance, List<string> oldStarterKidCodes, Promocode promocode);
    Task<Application> CreateApplicationForPWD(User user, Distance distance, List<string> oldStarterKidCodes);
    Application CreateApplicationViaMoney(User user, Distance distance, List<string> oldStarterKidCodes);
    Application IssueStarterKit(Application application, string? fullNameRecipient, StartKitEnum starterKit);
    Application AssignNumber(Application application, Distance distance);
}