using Business;
using Models;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Models.ViewModels;
using DataBases;

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
        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userName)) return RedirectToAction("Index", "Home");
            account? acc = JsonConvert.DeserializeObject<account>(userName);
            if (acc == null) return RedirectToAction("Index", "Home");
            List<shoppingCartViewModel> MyProducts=await _orderBusiness.GetList(acc.pk);
            ViewBag.MyProducts = MyProducts.Select(x => new
            {
                x.pk,
                x.name,
                x.price,
                x.count,
                pic = Url.Content("~/files/pic/") + x.pic,
            }).Json();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int pk)
        {
            try
            {
                await _orderBusiness.Delete(pk);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }
    }
}
