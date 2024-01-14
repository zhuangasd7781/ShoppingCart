using DataBases;
using Models;
using Org.BouncyCastle.Crypto.Generators;
//using dekKBS_MVC.DB;

namespace Service
{
    public class accountService
    {
        private readonly IDB _db;
        public accountService(IDB db)
        {
            _db = db;
        }
        #region
        public async Task<Models.account?> Get(uint pk)
        {
            string sql = @"
            SELECT `pk`, `id`, `pwd`, `name`, `phone`, `role`, `date`, `status`
            FROM `account`
            WHERE `pk` = ?pk
            ;";
            return (await _db.GetListAsync<Models.account>(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            })).SingleOrDefault();
        }

        public async Task<Models.account?> Get(string Acc)
        {
            string sql = @"
            SELECT `pk`, `id`, `pwd`, `name`, `phone`, `role`, `date`, `status`
            FROM `account`
            WHERE `id` = ?id
            ;";
            return (await _db.GetListAsync<Models.account>(sql, new Dictionary<string, object?>
            {
                { "?id", Acc },
            })).SingleOrDefault();
        }
        public async Task<uint> Insert(Models.account obj, bool autoIncrement = true)
        {
            string sql = @"
            INSERT INTO `account`
            (`pk`, `id`, `pwd`, `name`, `phone`, `role`, `date`, `status`)
            VALUES
            (?pk, ?id, ?pwd, ?name, ?phone, ?role, ?date, ?status);

            SELECT LAST_INSERT_ID();
            ";
            return (await _db.ExecuteScalarAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", autoIncrement ? null : (object)obj.pk },
                { "?id", obj.id },
                { "?pwd", obj.pwd },
                { "?name", obj.name },
                { "?phone", obj.phone },
                { "?role", obj.role },
                { "?date", obj.date },
                { "?status", obj.status }
            })).UInt();
        }

        public async Task<int> Update(Models.account obj)
        {
            string sql = @"
            UPDATE `account`
            SET `pk` = ?pk 
            ,`id` = ?id 
            ,`pwd` = ?pwd 
            ,`name` = ?name 
            ,`phone` = ?phone 
            ,`role` = ?role 
            ,`date` = ?date 
            ,`status` = ?status
            WHERE `pk` = ?pk
            ;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?id", obj.id },
                { "?pwd", obj.pwd },
                { "?name", obj.name },
                { "?phone", obj.phone },
                { "?role", obj.role },
                { "?date", obj.date },
                { "?status", obj.status },

                { "?pk", obj.pk },
            });
        }

        public async Task<int> Delete(uint pk)
        {
            string sql = @"
            DELETE FROM `account`
            WHERE `pk` = ?pk
            ;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            });
        }
        public async Task<List<Models.account>> GetList()
        {
            string sql = @"
            SELECT `pk`, `id`, `pwd`, `name`, `phone`, `role`, `date`, `status`
            FROM `account`
            WHERE 1
            ;";
            return await _db.GetListAsync<Models.account>(sql, new Dictionary<string, object?>
            {
                //{ "?xxx", xxx },
                //{ "?keyword", "%" + keyword + "%" },
            });
        }

        #endregion
    }
}
