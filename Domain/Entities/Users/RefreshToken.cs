using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users
{
    public class RefreshToken : IdentityUserToken<long>
    {
        public long Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }
}

