using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNetCommon.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCommon.Data.Domain.Business
{
    public class RepositoryBase<TEntity>
    {
        public virtual async Task<SearchResult> SkipTake(IQueryable<object> query, int skip, int take)
        {
            var result = new SearchResult();
            if (take > 0)
            {
                // total = skip + take + 1;
                result.Total = await query.CountAsync();
                query = query.Skip(skip).Take(take);
            }

            result.Items = await query.Select(x => (object)x).ToListAsync();
            return result;
        }

        public virtual async Task<SearchResult> SkipTakeLazy(IQueryable<object> query, int skip, int take)
        {
            var items = await query.Skip(skip).Take(take + 1).Select(x => (object)x).ToListAsync();
            return new SearchResult
            {
                Total = skip + items.Count,
                Items = items.Take(take).ToList(),
            };
        }

        public Expression<Func<TEntity, T>> Projection<T>(Expression<Func<TEntity, T>> projection)
        {
            return projection;
        }

        public Expression<Func<TEntity, object>> SortField(string orderBy, params Expression<Func<TEntity, object>>[] fields)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                foreach (var field in fields)
                {
                    string name;
                    if (field.Body is MemberExpression)
                    {
                        name = (field.Body as MemberExpression).Member.Name;
                    }
                    else
                    {
                        try
                        {
                            name = (field.Body as dynamic).Operand.Member.Name;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    if (name.Equals(orderBy, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return field;
                    }
                }
            }

            return null;
        }
    }
}
