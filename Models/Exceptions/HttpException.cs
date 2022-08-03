using System;
using System.Net;

namespace MarathonApp.Models.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public new Exception InnerException { get; set; }

        public HttpException(string message, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
        {
            ErrorMessage = message;
            StatusCode = statusCode;
            InnerException = innerException;
        }

        public HttpException(Exception ex, HttpStatusCode statusCode) : base(ex.Message, ex)
        {
            ErrorMessage = ex.Message;
            StatusCode = statusCode;
            InnerException = ex.InnerException;
        }

        public HttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            ErrorMessage = message;
            StatusCode = statusCode;
        }
    }
}

