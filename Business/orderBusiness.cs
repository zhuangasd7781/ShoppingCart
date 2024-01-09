using DataBases;
using Models;
using Service;
using Serilog;
using Models.ViewModels;

namespace Business
{
    public class orderBusiness
    {
        orderService _orderService;
        public orderBusiness(IDB _db)
        {
            _orderService = new orderService(_db);
        }
        #region
        public async Task<int> Insert(int accPk, int commodityPk, int count)
        {
            try
            {
                order dm = new order()
                {
                    acc_fk = accPk,
                    commodity_fk = commodityPk,
                    count = count,
                    status = 0,
                };
                uint insertPk = await _orderService.Insert(dm);
                return insertPk.Int();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }

        }
        public async Task<List<shoppingCartViewModel>> GetList(int accPk)
        {
            try
            {
                return await _orderService.GetList(accPk);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Delete(int pk)
        {
            try
            {
                return await _orderService.Delete(pk.UInt());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
