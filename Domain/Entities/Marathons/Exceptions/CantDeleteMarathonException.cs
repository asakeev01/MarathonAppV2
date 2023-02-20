using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Marathons.Exceptions;

public class CantDeleteMarathonException : DomainException
{
    public CantDeleteMarathonException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.CantDeleteMarathonError], 30)
    {
    }
}

