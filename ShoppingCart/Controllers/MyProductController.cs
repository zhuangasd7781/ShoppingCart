using Business;
using Models;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using DataBases;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class MyProductController : Controller
    {
        commodityBusiness _commodityBusiness;
        public MyProductController(IDB _db) 
        {
            _commodityBusiness = new commodityBusiness(_db);
        }
        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userName)) return RedirectToAction("Index", "Home");
            account? acc = JsonConvert.DeserializeObject<account>(userName);
            if (acc == null) return RedirectToAction("Index", "Home");
            List<commodity> MyProducts =await _commodityBusiness.GetList(acc.pk);
            ViewBag.MyProducts = MyProducts.Select(x => new
            {
                pic=Url.Content("~/files/pic/")+ x.pic,
                x.pk,
                x.name,
                x.price,
            }).Json();
            return View();
        }
        [HttpPost]
        public async Task<int> Set(string commodityJson, int AccPk, IFormFile file)
        //public async Task<int> Set([FromForm] company _company, [FromForm] int AccPk, [FromForm] IFormFile file)
        {
            return await _commodityBusiness.Set(commodityJson, AccPk, file);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int pk)
        {
            try
            {
                await _commodityBusiness.Delete(pk);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }
    }
}
