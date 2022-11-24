namespace Domain.Common.Contracts;

public interface IUnitOfWork : IDisposable
{
    IMarathonRepository MarathonRepository { get; }

    IDistanceRepository DistanceRepository { get; }

    IMarathonTranslationRepository MarathonTranslationRepository { get; }

    ISavedFileRepository SavedFileRepository { get; }

    IPartnerRepository PartnerRepository { get; }

    IUserRepository UserRepository { get; }

    IRefreshTokenRepository RefreshTokenRepository { get; }

    IDocumentRepository DocumentRepository { get; }

    IStatusRepository StatusRepository { get; }
    IVoucherRepository VoucherRepository { get; }
    IPromocodeRepository PromocodeRepository { get; }

    IApplicationRepository ApplicationRepository { get; }

    IDistanceForPwdRepository DistanceForPwdRepository { get; }

    void Save();
    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitAsync(bool save = false);
    Task RollbackAsync();
}