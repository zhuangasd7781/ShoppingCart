using DataBases;
using Models;
using Service;
using Serilog;
using Models.ViewModels;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Security.Policy;

namespace Business
{
    public class orderBusiness
    {
        orderService _orderService;
        commodityService _commodityService;
        public orderBusiness(IDB _db)
        {
            _orderService = new orderService(_db);
            _commodityService = new commodityService(_db);
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

        public async Task<List<shoppingCartViewModel>> GetList(string json, string picPath)
        {
            List<CartItem>? caritems = JsonConvert.DeserializeObject<List<CartItem>>(json);
            List<shoppingCartViewModel> shoppingCart = new List<shoppingCartViewModel>();
            if (caritems.Count <= 0) return shoppingCart;

            foreach (var item in caritems)
            {
                // 假设有一个方法 GetProductById 来获取商品详情
                var product = await _commodityService.Get(item.commodityPk.UInt());
                if (product != null)
                {
                    shoppingCart.Add(new shoppingCartViewModel
                    {
                        pk = product.pk,
                        name = product.name,
                        price = product.price,
                        pic = product.pic, //product.pic, // 确保图片路径是正确的
                        count = item.count
                    }); ;
                }
            }
            return shoppingCart;// new List<shoppingCartViewModel>();
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
