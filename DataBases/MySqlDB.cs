using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DataBases
{
    public class MySqlDB : IDB
    {
        private string _ConnectionString;
        public MySqlDB(string? connectionString)
        {
            _ConnectionString = connectionString.Text();
        }
        public async Task<object?> ExecuteScalarAsync(string sql, Dictionary<string, object?> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                foreach (var items in parameters)
                {
                    cmd.Parameters.AddWithValue(items.Key, items.Value);
                }
                await conn.OpenAsync();
                return await cmd.ExecuteScalarAsync();
            }
            throw new NotImplementedException();
        }
        public async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object?> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                foreach (var items in parameters)
                {
                    cmd.Parameters.AddWithValue(items.Key, items.Value);
                }
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<T>> GetListAsync<T>(string sql, Dictionary<string, object?> parameters) where T : new()
        {
            using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                foreach (var items in parameters)
                {
                    cmd.Parameters.AddWithValue(items.Key, items.Value);
                }
                await conn.OpenAsync();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    return await GetListAsync<T>(dr);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetListAsync<T>(DbDataReader dr) where T : new()
        {
            List<T> list = new List<T>();
            var prop = typeof(T).GetProperties();
            while (await dr.ReadAsync())
            {
                var obj = new T();
                foreach (var item in prop)
                {
                    var value = dr[item.Name];
                    if (item.PropertyType == typeof(int) || item.PropertyType == typeof(uint?))
                        item.SetValue(obj, value);
                    else
                    {
                        if (item.PropertyType == typeof(DateTime))
                            item.SetValue(obj, value.Date("yyyy-MM-dd HH:mm:ss"));
                        else
                            item.SetValue(obj, value.Text());
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }


}
