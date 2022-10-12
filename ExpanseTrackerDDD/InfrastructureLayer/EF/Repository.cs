using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ETContext Context;
        public Repository(ETContext context)
        {
            Context = context;
        }

        protected static List<TEntity> _entities = new List<TEntity>();

        public TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            Debug.WriteLine("tutaj przyszło");
            var a = Context.Set<TEntity>().Where(expression);
            IList < TEntity > result = a == null ? new List<TEntity>() :  a.ToList();
            return result;
        }
        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}
