using System;
using Microsoft.AspNetCore.Authorization;

namespace MarathonApp.BLL.Policies
{
    public class UserPolicy : IAuthorizationRequirement
    {
        public bool IsNew { get; }

        public UserPolicy(bool isNew)
        {
            IsNew = isNew;
        }
    }
}

