using System;
using System.Linq.Expressions;

namespace DotNetCommon.Data.Domain.Entitites;

/// <summary>
/// Default MongoDB entity base interface
/// </summary>
/// <typeparam name="TPrimaryKey">Primary Key Type</typeparam>
public interface IMongoDBBaseEntity<TPrimaryKey>
{
    Expression<Func<TPrimaryKey, bool>> PrimaryPredicate();
}
