using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity: AggregateRoot
    {
        TEntity Get(Guid id);
        IList<TEntity> GetAll();
        IList<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Insert(TEntity entity);
        void Delete(TEntity entity);

    }
}
