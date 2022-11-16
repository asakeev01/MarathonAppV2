using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class InvalidHeadersInExcelException : DomainException
{
    public InvalidHeadersInExcelException() : base("Headers in excel file are invalid.", 8)
    {
    }
}
