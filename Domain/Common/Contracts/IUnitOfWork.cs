namespace Domain.Common.Contracts;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    
    ITransactionRepository TransactionRepository { get; }
    
    IMarathonRepository MarathonRepository { get; }

    IDistanceRepository DistanceRepository { get; }

    IMarathonTranslationRepository MarathonTranslationRepository { get; }

    ISavedFileRepository SavedFileRepository { get; }

    IPartnerRepository PartnerRepository { get; }

    IUserRepository UserRepository { get; }

    IRefreshTokenRepository RefreshTokenRepository { get; }

    void Save();
    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitAsync(bool save = false);
    Task RollbackAsync();
}