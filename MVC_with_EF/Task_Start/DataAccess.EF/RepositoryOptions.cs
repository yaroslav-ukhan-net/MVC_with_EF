using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.EF
{
    public class RepositoryOptions<TEntity> : UniversityRepository, IRepository<TEntity> where TEntity : class 
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;
        public RepositoryOptions(string connectionString, DbContext context) : base (connectionString)
        {
            _context = context;
            context.Database.EnsureCreated();
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(int id)
        {

            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
