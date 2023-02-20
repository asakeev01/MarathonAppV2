using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class InvalidHeadersInExcelException : DomainException
{
    public InvalidHeadersInExcelException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.InvalidHeadersInExcelError], 4)
    {
    }
}
