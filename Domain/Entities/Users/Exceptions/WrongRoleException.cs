using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Users.Exceptions
{
    public class WrongRoleException : DomainException
    {
        public WrongRoleException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.WrongRoleError], 19)
        {
        }
    }
}

