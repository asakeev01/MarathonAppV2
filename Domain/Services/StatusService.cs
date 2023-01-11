using System;
using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class StatusService : IStatusService
{
    public StatusService()
    {
    }

    public List<StatusComment> SetUserStatus(User user, Document document, Status status, StatusesEnum newStatus, List<Comment> comments)
    {
        status.CurrentStatus = newStatus;
        var statusComments = new List<StatusComment>();
        if(comments != null)
            foreach (var comment in comments)
            {
                var statusComment = new StatusComment()
                {
                    Status = status,
                    Comment = comment
                };
                statusComments.Add(statusComment);
            }

        if (status.CurrentStatus == StatusesEnum.Confirmed && document.DisabilityPath != null)
        {
            user.IsDisable = true;
        }
        else
        {
            user.IsDisable = false;
        }
        if(status.CurrentStatus == StatusesEnum.Confirmed)
        {
            document.IsArchived = true;
            var doc = new Document()
            {
                FrontPassportPath = document.FrontPassportPath,
                InsurancePath = document.InsurancePath,
                DisabilityPath = document.DisabilityPath,
                BackDisabilityPath = document.BackDisabilityPath,
                BackInsurancePath = document.BackInsurancePath,
                BackPassportPath = document.BackPassportPath,
                IsArchived = false
            };
            user.Documents.Add(doc);
        }
        return statusComments;
    }
}


