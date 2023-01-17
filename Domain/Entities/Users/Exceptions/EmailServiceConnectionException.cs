using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailServiceConnectionException : DomainException
    {
        public EmailServiceConnectionException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.EmailServiceConnectionError], 12)
        {
        }
    }
}

