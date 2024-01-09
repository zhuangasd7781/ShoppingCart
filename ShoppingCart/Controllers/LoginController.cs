using Business;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DataBases;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        accountBusiness _accountBusiness;
        public LoginController(IDB _db)
        {
            _accountBusiness = new accountBusiness(_db);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoggedIn(account account)
        {
            try
            {
                var user = await _accountBusiness.Get(account.id);
                if (user == null)
                {
                    throw new Exception("帳號不存在");
                }
                if (account.pwd.MD5() != user.pwd)
                {
                    throw new Exception("密碼錯誤");
                }
                await HttpContext.SignIn(user);
                return Ok();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(Json(ex.Message));
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveAccount(account account)
        {
            try
            {                   
                await _accountBusiness.Insert(account);
                return Ok();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(Json(ex.Message));
            }
        }
    }
}
