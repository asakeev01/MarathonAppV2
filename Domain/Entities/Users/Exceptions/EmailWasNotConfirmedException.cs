using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailWasNotConfirmedException : DomainException
    {
        public EmailWasNotConfirmedException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.EmailWasNotConfirmedError], 13)
        {
        }
    }
}

