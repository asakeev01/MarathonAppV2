using System.Transactions;
using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly UserManager<User> _userManager;

    private IAccountRepository? _accountRepository;
    private ITransactionRepository? _transactionRepository;
    private IMarathonRepository? _marathonRepository;
    private IDistanceRepository? _distanceRepository;
    private IMarathonTranslationRepository? _marathonTranslationRepository;
    private ISavedFileRepository? _savedFileRepository;
    private IUserRepository? _userRepository;
    private IPartnerRepository? _partnerRepository;
    private IRefreshTokenRepository? _refreshTokenRepository;
    private IDocumentRepository? _documentRepository;

    private bool disposed = false;


    public UnitOfWork(UserManager<User> userManager, AppDbContext context, IStringLocalizer<SharedResource> localizer)
    {
        _context = context;
        _localizer = localizer;
        _userManager = userManager;
    }
        
    public IAccountRepository AccountRepository
    {
        get
        {
            _accountRepository ??= new AccountRepository(_context, _localizer);
            return _accountRepository;
        }
    }
    
    public ITransactionRepository TransactionRepository
    {
        get
        {
            _transactionRepository ??= new TransactionRepository(_context, _localizer);
            return _transactionRepository;
        }
    }

    public IMarathonRepository MarathonRepository
    {
        get
        {
            _marathonRepository ??= new MarathonRepository(_context, _localizer);
            return _marathonRepository;
        }
    }

    public IDistanceRepository DistanceRepository
    {
        get
        {
            _distanceRepository ??= new DistanceRepository(_context, _localizer);
            return _distanceRepository;
        }
    }

    public IMarathonTranslationRepository MarathonTranslationRepository
    {
        get
        {
            _marathonTranslationRepository ??= new MarathonTranslationRepository(_context, _localizer);
            return _marathonTranslationRepository;
        }
    }

    public ISavedFileRepository SavedFileRepository
    {
        get
        {
            _savedFileRepository ??= new SavedFileRepository(_context, _localizer);
            return _savedFileRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            _userRepository ??= new UserRepository(_userManager, _context, _localizer);
            return _userRepository;
        }
    }

    public IPartnerRepository PartnerRepository
    {
        get
        {
            _partnerRepository ??= new PartnerRepository(_context, _localizer);
            return _partnerRepository;
        }
    }

    public IRefreshTokenRepository RefreshTokenRepository
    {
        get
        {
            _refreshTokenRepository ??= new RefreshTokenRepository(_context, _localizer);
            return _refreshTokenRepository;
        }
    }

    public IDocumentRepository DocumentRepository
    {
        get
        {
            _documentRepository ??= new DocumentRepository(_context, _localizer);
            return _documentRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public async Task BeginTransactionAsync()  
    {  
        await _context.Database.BeginTransactionAsync();  
    } 
    
    public async Task CommitAsync(bool save = false)  
    {
        if (save)
        {
            await _context.SaveChangesAsync();
        }
        
        await _context.Database.CommitTransactionAsync();  
    } 
    
    public async Task RollbackAsync()  
    {  
        await _context.Database.RollbackTransactionAsync();  
    }  
}