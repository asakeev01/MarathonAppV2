using System;
using System.Text.Json;

namespace Models.Exceptions
{
    public class ErrorDatailsModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

