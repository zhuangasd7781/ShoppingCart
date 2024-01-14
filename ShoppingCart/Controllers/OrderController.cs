using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
