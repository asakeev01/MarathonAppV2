using System;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class RefreshToken : IdentityUserToken<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }
}

