using System;
using Domain.Entities.Users.UserEnums;

namespace Domain.Entities.Users
{
    public class Status
    {
        public long Id { get; set; }
        public StatusesEnum CurrentStatus { get; set; }
        public CommentsEnum Comment { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}

