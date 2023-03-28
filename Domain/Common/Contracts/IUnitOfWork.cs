using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

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
    
    IVoucherRepository VoucherRepository { get; }
    
    IPromocodeRepository PromocodeRepository { get; }

    IApplicationRepository ApplicationRepository { get; }

    IStatusRepository StatusRepository { get; }

    ICommentRepository CommentRepository { get; }

    IStatusCommentRepository StatusCommentRepository { get; }

    IPartnerCompanyRepository PartnerCompanyRepository { get; }

    IEmailRepository? EmailRepository { get; }

    IResultRepository? ResultRepository { get; }

    void Save();
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel? level);
    Task CommitAsync(bool save = false);
    Task RollbackAsync();
}