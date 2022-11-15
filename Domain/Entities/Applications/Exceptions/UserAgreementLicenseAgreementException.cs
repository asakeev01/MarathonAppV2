using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class UserAgreementLicenseAgreementException : DomainException
{
    public UserAgreementLicenseAgreementException() : base("You have not confirmed the user agreement", 7)
    {
    }
}
