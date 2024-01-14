using Business;
using Models;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Models.ViewModels;
using DataBases;
using System.Collections.Generic;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        orderBusiness _orderBusiness;
        public ShoppingCartController(IDB _db)
        {
            _orderBusiness = new orderBusiness(_db);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cartCookie = HttpContext.Request.Cookies["cart"];

            if (cartCookie == null)
            {
                ViewBag.MyProducts = new List<shoppingCartViewModel>();
                return View();
            }

            List<shoppingCartViewModel> MyProducts = await _orderBusiness.GetList(cartCookie, Url.Content("~/files/pic/"));
            ViewBag.MyProducts = MyProducts.Select(x => new
            {
                x.pk,
                x.name,
                x.price,
                x.count,
                pic = Url.Content("~/files/pic/") + x.pic,
            }).Json();

            // var userName = HttpContext.User.Identity.Name;
            // if (string.IsNullOrEmpty(userName)) return RedirectToAction("Index", "Home");
            // account? acc = JsonConvert.DeserializeObject<account>(userName);
            // if (acc == null) return RedirectToAction("Index", "Home");
            // List<shoppingCartViewModel> MyProducts = await _orderBusiness.GetList(4);

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Delete(int pk)
        {
            try
            {
                var cartCookie = HttpContext.Request.Cookies["cart"];

                if (!string.IsNullOrEmpty(cartCookie))
                {
                    // 解析 Cookie 中的购物车数据
                    List<CartItem>? cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartCookie);
                    // 移除特定 commodityPk 的项
                    cartItems.RemoveAll(item => item.commodityPk == pk);
                    // 将更新后的购物车数据转换为 JSON 字符串
                    string updatedCartJson = JsonConvert.SerializeObject(cartItems);
                    // 更新 Cookie
                    HttpContext.Response.Cookies.Append("cart", updatedCartJson, new CookieOptions {Expires = DateTime.Now.AddDays(7) });
                }

                //await _orderBusiness.Delete(pk);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }
    }
}
