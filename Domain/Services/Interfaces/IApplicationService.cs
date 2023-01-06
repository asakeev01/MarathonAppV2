using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Distances;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;

namespace Domain.Services.Interfaces;

public interface IApplicationService
{
    Task<Application> CreateApplication(User user, Distance distance, List<string> oldStarterKidCodes, Promocode promocode);
    Task<Application> CreateApplicationForPWD(User user, DistanceForPWD distance, List<string> oldStarterKidCodes);
    Task<Application> VoucherApplication(User user, Distance distance, DistanceAge distanceAge, Promocode promocode);
    Application IssueStarterKit(Application application, string? fullNameRecipient, StartKitEnum starterKit);
    Task<string> CreatePaymentAsync(Application application);
    Application CreateApplicationViaMoney(User user, Distance distance, List<string> oldStarterKidCodes);
}