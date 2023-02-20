using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class InvalidTokenException : DomainException
    {
        public InvalidTokenException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.InvalidTokenError], 14)
        {
        }
    }
}

