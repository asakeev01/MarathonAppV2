﻿using System;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Statuses;
using Domain.Entities.Users;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class StatusCommentRepository : BaseRepository<StatusComment>, IStatusCommentRepository
{
    public StatusCommentRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}


