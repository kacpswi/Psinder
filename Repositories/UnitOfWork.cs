﻿using AutoMapper;
using Psinder.Data;
using Psinder.Repositories.Interfaces;

namespace Psinder.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PsinderDb _context;

        public UnitOfWork(PsinderDb context)
        {
            _context = context;
        }

        public IShelterRepository ShelterRepository => new ShelterRepository(_context);
        public IAnimalRepository AnimalRepository => new AnimalRepository(_context);
        public ILikeRepository LikeRepository => new LikeRepository(_context);
        public IMessageRepository MessageRepository => new MessageRepository(_context);
        public IUserRepository UserRepository => new UserRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
