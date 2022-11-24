using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class InvalidSheetNameException : DomainException
{
    public InvalidSheetNameException() : base("Invalid sheet name for excel file you uploaded.", 9)
    {
    }
}
