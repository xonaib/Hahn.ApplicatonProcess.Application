using Hahn.ApplicatonProcess.December2020.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Linq.Expressions;
using Hahn.ApplicatonProcess.December2020.Domain.Contracts;

namespace Hahn.ApplicatonProcess.December2020.Domain.Repository
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        protected DBContext RepositoryContext { get; set; }
        public RepositoryBase(DBContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>(); //.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression); //.AsNoTracking();
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            RepositoryContext.SaveChanges();
        }
    }
}
