using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCommon.Data.Domain.Interfaces;

public interface IElasticSearchBaseRepository<TDocument, TPrimaryKey> : ICrudRepositoryBase<TDocument>
    where TDocument : class, new()
{
    Task<int> DeleteBy(TPrimaryKey id);

    Task<TDocument> GetBy(TPrimaryKey id);

    Task<List<TDocument>> GetMany(List<TPrimaryKey> ids);

    Task<int> EditOnly(TPrimaryKey id, object partialEntity);

    // Task<(List<TDocument> Data, long Total)> Search(int from, int size, Func<QueryContainerDescriptor<TDocument>, QueryContainer> query, Expression<Func<TDocument, object>>[] selectFields, Field sortField, bool isAscending);
}
