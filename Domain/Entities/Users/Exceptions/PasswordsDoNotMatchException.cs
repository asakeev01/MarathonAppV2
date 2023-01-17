using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class PasswordsDoNotMatchException : DomainException
    {
        public PasswordsDoNotMatchException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.PasswordsDoNotMatchError], 15)
        {
        }
    }
}

