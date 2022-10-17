using System;
namespace Domain.Common.Options
{
    public class SecurityTokenOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret{ get; set; }
        public int ExpiresIn { get; set; }
    }
}

