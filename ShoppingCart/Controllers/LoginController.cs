using Business;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DataBases;
using Org.BouncyCastle.Crypto.Generators;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        accountBusiness _accBusiness;
        public LoginController(IDB _db)
        {
            _accBusiness = new accountBusiness(_db);
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
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Json(errors));
                    // return BadRequest(Json(errors.FirstOrDefault())); // 返回第一个错误
                }

                var user = await _accBusiness.Get(account.id);
                if (user == null)
                {
                    throw new Exception("帳號不存在");
                }
                if (!_accBusiness.Verify(account.pwd, user.pwd))
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
                await _accBusiness.Insert(account);
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
