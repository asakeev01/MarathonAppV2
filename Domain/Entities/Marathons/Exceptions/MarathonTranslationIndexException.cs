using Domain.Common.Exceptions;

namespace Domain.Entities.Marathons.Exceptions;

public class MarathonTranslationIndexException : DomainException
{
    public MarathonTranslationIndexException() :
        base("Index exception", 3)
    {
    }
}
