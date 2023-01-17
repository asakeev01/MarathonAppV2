using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;


namespace Domain.Entities.Applications.Exceptions;

public class InvalidSheetNameException : DomainException
{
    public InvalidSheetNameException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.InvalidSheetNameError], 5)
    {
    }
}
