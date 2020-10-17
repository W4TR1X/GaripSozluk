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
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        private readonly GaripSozlukLogDbContext _context;
        private readonly DbSet<Log> _dbSet;
        public LogRepository(GaripSozlukLogDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Log>();
        }
    }
}
