using System;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;

namespace Domain.Entities.Statuses;

public class Status
{
    public long Id { get; set; }
    public StatusesEnum CurrentStatus { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public ICollection<StatusComment>? StatusComments {get; set;}
}


