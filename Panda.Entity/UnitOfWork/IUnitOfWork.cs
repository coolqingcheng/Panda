﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Panda.Entity.UnitOfWork;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollBackAsync();
}

public class EFUnitOfWork : IUnitOfWork
{
    private readonly PandaContext _context;

    private IDbContextTransaction? _transaction = null;

    private int _index = 0;

    public EFUnitOfWork(PandaContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        _index += 1;
    }

    public async Task CommitAsync()
    {
        if (_index > 1)
        {
            _index -= 1;
            return;
        }

        if (_transaction != null)
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            _index = 0;
        }

        ;
    }

    public async Task RollBackAsync()
    {
        if (_transaction != null) await _transaction.RollbackAsync();
    }
}