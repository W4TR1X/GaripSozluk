using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GaripSozluk.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly GaripSozlukLogDbContext _context;
        private readonly DbSet<Log> _dbSet;
        public LogRepository(GaripSozlukLogDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Log>();
        }

        public Log Add(Log entity)
        {
            var entityEntry = _dbSet.Add(entity);
            return entityEntry.Entity;
        }

        public Log Get(Expression<Func<Log, bool>> expression)
        {
            var result = _dbSet.Where(expression).FirstOrDefault();
            return result;
        }

        public IQueryable<Log> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<Log> Where(Expression<Func<Log, bool>> expression, List<string> includes)
        {
            var query = _dbSet.Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public Log Update(Log entity)
        {
            var entityEntry = _dbSet.Update(entity);
            return entityEntry.Entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Log entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
