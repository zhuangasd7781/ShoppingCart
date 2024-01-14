using System.Data;
using System.Data.Common;

namespace DataBases
{
    public interface IDB
    {
        public Task<object?> ExecuteScalarAsync(string sql, Dictionary<string, object?> parameters);
        public Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object?> parameters);
        public Task<List<T>> GetListAsync<T>(string sql, Dictionary<string, object?> parameters) where T : new();
        public Task<List<T>> GetListAsync<T>(DbDataReader dr) where T : new();


        // 新增方法
        // public Task BeginTransactionAsync();
        // public Task CommitTransactionAsync();
        // public Task RollbackTransactionAsync();
        // public Task<DbDataReader> ExecuteReaderAsync(string sql, Dictionary<string, object?> parameters);
        // public Task<List<T>> GetPagedListAsync<T>(string sql, Dictionary<string, object?> parameters, int pageNumber, int pageSize) where T : new();
        // public Task BulkInsertAsync<T>(IEnumerable<T> entities) where T : class;
        // public Task BulkUpdateAsync<T>(IEnumerable<T> entities) where T : class;
        // public Task BulkDeleteAsync<T>(IEnumerable<T> entities) where T : class;
        // public Task<T> GetCachedAsync<T>(string cacheKey, Func<Task<T>> retrieveData) where T : class;
    }
}
