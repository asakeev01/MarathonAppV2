using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class UserDoesNotExistException : DomainException
    {
        public UserDoesNotExistException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.UserDoesNotExistError], 17)
        {
        }
    }
}

