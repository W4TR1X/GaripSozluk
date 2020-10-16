using GaripSozluk.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GaripSozluk.Data.Interfaces
{
    public interface ILogRepository
    {
        Log Add(Log entity);
        Log Update(Log entity);
        IQueryable<Log> GetAll();
        IQueryable<Log> Where(Expression<Func<Log, bool>> expression, List<string> Includes = null);
        Log Get(Expression<Func<Log, bool>> expression);

        void Remove(Log entity);

        void Save();
    }
}
