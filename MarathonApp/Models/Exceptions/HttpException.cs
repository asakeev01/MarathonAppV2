using System;
using System.Net;

namespace MarathonApp.Models.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public new Exception InnerException { get; set; }

        public HttpException(string message, Exception innerException, HttpStatusCode statusCode)
        {
            ErrorMessage = message;
            StatusCode = statusCode;
            InnerException = innerException;
        }
    }
}

