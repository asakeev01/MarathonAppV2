using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Marathons.Exceptions
{
    public class MarathonTranslationIndexException : DomainException
    {
        public MarathonTranslationIndexException() :
            base("Index exception", 3)
        {
        }
    }
}
