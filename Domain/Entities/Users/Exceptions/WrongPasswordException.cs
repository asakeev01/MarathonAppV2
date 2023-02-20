using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class WrongPasswordException : DomainException
    {
        public WrongPasswordException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.WrongPasswordError], 18)
        {
        }
    }
}

