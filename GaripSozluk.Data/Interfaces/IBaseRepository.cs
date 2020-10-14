using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GaripSozluk.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,List<string> Includes = null);
        TEntity Get(Expression<Func<TEntity, bool>> expression);

        void Remove(TEntity entity);

        void Save();
    }
}
