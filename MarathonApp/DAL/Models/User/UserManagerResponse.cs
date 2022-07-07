using System;
namespace MarathonApp.DAL.Models.User
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

