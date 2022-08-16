using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    public class RefreshTokenModel
    {
        public class Get
        {
            [MaxLength(450)]
            public string UserId { get; set; }

            [MaxLength(450)]
            public string Name { get; set; }

            public DateTime ExpirationDateUtc { get; set; }
        }

        public class Add
        {
            [MaxLength(450)]
            public string UserId { get; set; }

            [MaxLength(450)]
            public string Name { get; set; }
        }
    }
}

