using Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Business;
using System.Collections.Generic;
using DataBases;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        commodityBusiness _commodityBusiness;
        orderBusiness _orderBusiness;
        public HomeController(ILogger<HomeController> logger, IDB _db)
        {
            _commodityBusiness = new commodityBusiness(_db);
            _orderBusiness=new orderBusiness(_db);
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<commodity> Products = await _commodityBusiness.GetList();
            ViewBag.Products = Products.Select(x => new
            {
                x.pk,
                pic = Url.Content("~/files/pic/") + x.pic,
                x.name,
                x.price,
            }).Json();
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "LogIn");
        }
        [HttpPost]
        public async Task<IActionResult> addToTheCart(int accPk,int commodityPk,int count)
        {
            try
            {
                await _orderBusiness.Insert(accPk, commodityPk, count);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }
    }
}