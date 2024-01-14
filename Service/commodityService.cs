using DataBases;

namespace Service
{
    public class commodityService
    {
        public IDB _db;
        public commodityService(IDB db)
        {
            _db = db;
        }
        #region
        public async Task<Models.commodity?> Get(uint pk)
        {
            string sql = @"
                SELECT `acc_fk`, `pk`, `name`, `pic`, `price`
                FROM `commodity`
                WHERE `pk` = ?pk
                ;";
            return (await _db.GetListAsync<Models.commodity>(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            })).SingleOrDefault();
        }

        public async Task<Models.commodity?> Get(string Acc)
        {
            string sql = @"
SELECT `acc_fk`, `pk`, `name`, `pic`, `price`
FROM `commodity`
WHERE `id` = ?id
;";
            return (await _db.GetListAsync<Models.commodity>(sql, new Dictionary<string, object?>
            {
                { "?id", Acc },
            })).SingleOrDefault();
        }
        public async Task<uint> Insert(Models.commodity obj, bool autoIncrement = true)
        {
            string sql = @"
INSERT INTO `commodity`
(`acc_fk`, `pk`, `name`, `pic`, `price`)
VALUES
(?acc_fk, ?pk, ?name, ?pic, ?price);

SELECT LAST_INSERT_ID();
";
            return (await _db.ExecuteScalarAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", autoIncrement ? null : (object)obj.pk },
                { "?acc_fk", obj.acc_fk },
                { "?name", obj.name },
                { "?pic", obj.pic },
                { "?price", obj.price }
            })).UInt();
        }

        public async Task<int> Update(Models.commodity obj)
        {
            string sql = @"
UPDATE `commodity`
SET `acc_fk` = ?acc_fk ,
`pk` = ?pk ,
`name` = ?name ,
`pic` = ?pic ,
`price` = ?price
WHERE `pk` = ?pk
;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?acc_fk", obj.acc_fk },
                { "?name", obj.name },
                { "?pic", obj.pic },
                { "?price", obj.price },

                { "?pk", obj.pk },
            });
        }

        public async Task<int> Delete(uint pk)
        {
            string sql = @"
DELETE FROM `commodity`
WHERE `pk` = ?pk
;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            });
        }
        public async Task<List<Models.commodity>> GetList(int accPk=0)
        {
            string sql = @"
SELECT `acc_fk`, `pk`, `name`, `pic`, `price`
FROM `commodity`
WHERE 1
{acc_fk}
;".Replace("{acc_fk}",accPk==0?"": "AND `acc_fk` = ?acc_fk");
            return await _db.GetListAsync<Models.commodity>(sql, new Dictionary<string, object?>
            {
                //{ "?xxx", xxx },
                //{ "?keyword", "%" + keyword + "%" },
                { "?acc_fk", accPk },
            });
        }
        #endregion
    }
}
