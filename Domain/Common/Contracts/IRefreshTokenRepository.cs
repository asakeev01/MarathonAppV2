using System;
using Domain.Entities.Users;

namespace Domain.Common.Contracts
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
    }
}

