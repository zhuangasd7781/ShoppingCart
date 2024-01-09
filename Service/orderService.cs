using DataBases;
using Models;
using Models.ViewModels;

namespace Service
{
    public class orderService
    {
        public IDB _db;
        public orderService(IDB db)
        {
            _db = db;
        }
        #region
        public async Task<Models.order?> Get(uint pk)
        {
            string sql = @"
SELECT `pk`, `acc_fk`, `commodity_fk`, `status`, `count`
FROM `order`
WHERE `pk` = ?pk
;";
            return (await _db.GetListAsync<Models.order>(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            })).SingleOrDefault();
        }

        public async Task<Models.order?> Get(string Acc)
        {
            string sql = @"
SELECT `pk`, `acc_fk`, `commodity_fk`, `status`, `count`
FROM `order`
WHERE `id` = ?id
;";
            return (await _db.GetListAsync<Models.order>(sql, new Dictionary<string, object?>
            {
                { "?id", Acc },
            })).SingleOrDefault();
        }
        public async Task<uint> Insert(Models.order obj, bool autoIncrement = true)
        {
            string sql = @"
INSERT INTO `order`
(`pk`, `acc_fk`, `commodity_fk`, `status`, `count`)
VALUES
(?pk, ?acc_fk, ?commodity_fk, ?status, ?count);

SELECT LAST_INSERT_ID();
";
            return (await _db.ExecuteScalarAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", autoIncrement ? null : (object)obj.pk },
                { "?acc_fk", obj.acc_fk },
                { "?commodity_fk", obj.commodity_fk },
                { "?status", obj.status },
                { "?count", obj.count }
            })).UInt();
        }

        public async Task<int> Update(Models.order obj)
        {
            string sql = @"
UPDATE `order`
SET `pk` = ?pk ,
`acc_fk` = ?acc_fk ,
`commodity_fk` = ?commodity_fk ,
`status` = ?status ,
`count` = ?count
WHERE `pk` = ?pk
;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?acc_fk", obj.acc_fk },
                { "?commodity_fk", obj.commodity_fk },
                { "?status", obj.status },
                { "?count", obj.count },

                { "?pk", obj.pk },
            });
        }

        public async Task<int> Delete(uint pk)
        {
            string sql = @"
DELETE FROM `order`
WHERE `pk` = ?pk
;";
            return await _db.ExecuteNonQueryAsync(sql, new Dictionary<string, object?>
            {
                { "?pk", pk },
            });
        }
        public async Task<List<Models.order>> GetList()
        {
            string sql = @"
SELECT `pk`, `acc_fk`, `commodity_fk`, `status`, `count`
FROM `order`
WHERE 1
;";
            return await _db.GetListAsync<Models.order>(sql, new Dictionary<string, object?>
            {
                //{ "?xxx", xxx },
                //{ "?keyword", "%" + keyword + "%" },
            });
        }
        public async Task<List<shoppingCartViewModel>> GetList(int accPk)
        {
            string sql = @"
SELECT shoppingcart.order.pk,commodity.name,commodity.price,commodity.pic,shoppingcart.order.count FROM shoppingcart.order,commodity
where shoppingcart.order.commodity_fk=commodity.pk
AND shoppingcart.order.acc_fk = ?acc_fk;
;";
            return await _db.GetListAsync<shoppingCartViewModel>(sql, new Dictionary<string, object?>
            {
                //{ "?xxx", xxx },
                //{ "?keyword", "%" + keyword + "%" },
                { "?acc_fk", accPk },
            });
        }
        #endregion
    }
}
