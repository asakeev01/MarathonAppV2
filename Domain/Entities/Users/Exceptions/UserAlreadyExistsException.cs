using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.UserAlreadyExistsError], 16)
        {
        }
    }
}
