using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailAlreadyConfirmedException : DomainException
    {
        public EmailAlreadyConfirmedException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.EmailAlreadyConfirmedError], 11)
        {
        }
    }
}

