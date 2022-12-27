using System;
using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;

namespace Domain.Services.Interfaces;

public interface IStatusService
{
    List<StatusComment> SetUserStatus(User user, Document document, Status status, StatusesEnum newStatus, List<Comment> comments);
}


