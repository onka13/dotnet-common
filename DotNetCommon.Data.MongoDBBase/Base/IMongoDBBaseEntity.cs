using System;
using System.Linq.Expressions;

namespace DotNetCommon.Data.MongoDBBase.Base;

/// <summary>
/// Default elastic search entity interface
/// </summary>
/// <typeparam name="TPrimaryKey">Primary Key Type</typeparam>
public interface IMongoDBBaseEntity<TDocument>
{
    Expression<Func<TDocument, bool>> PrimaryPredicate();
}
