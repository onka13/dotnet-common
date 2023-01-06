using DotNetCommon.Data.Domain.Business.Transaction;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCommon.Data.EntityFrameworkBase.Base
{
    public interface IEntityFrameworkBaseRepository<TEntity> : ITransactionRepositoryBase<TEntity>
    {
        IQueryable<TEntity> FindAndIncludeBy<TProp>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProp>>[] include);
    }
}
