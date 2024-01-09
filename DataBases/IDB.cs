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
    }
}
