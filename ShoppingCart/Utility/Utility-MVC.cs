using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Models;
//namespace dekKBS_MVC.Utility
//{
public static partial class Utility
{
    #region HttpContext

    public static async Task SignIn(this HttpContext httpContext, account? admin_user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin_user.Json()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = false // true.自動登入
                });
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public static account? User(this HttpContext httpContext)
    {
        string? json = httpContext.User.Identity?.Name;
        return Convertor.FromJson<account>(json.Text());
    }

    #endregion

    #region Controller

    public static void TempData(this Controller controller, string key, object? value)
    {
        controller.TempData["Utility." + key] = value.Json();
    }

    public static T? TempData<T>(this Controller controller, string key)
    {
        return Convertor.FromJson<T>(controller.TempData["Utility." + key].Text());
    }

    public static void Session(this Controller controller, string key, string? value)
    {
        controller.HttpContext.Session.SetString("Utility." + key, value.Text());
    }

    public static string Session(this Controller controller, string key)
    {
        return controller.HttpContext.Session.GetString("Utility." + key).Text();
    }

    public static void SweetAlert(this Controller controller, string msg, string remark)
    {
        controller.TempData["Utility.SweetAlert"] = new string[] { msg, remark };
    }

    public static void Alert(this Controller controller, string msg)
    {
        controller.TempData["Utility.Alert"] = msg;
    }

    public static void Alert(this Controller controller, Exception ex)
    {
        if (ex is MyException ex1)
        {
            SweetAlert(controller, ex1.Message, ex1.Remark);
            return;
        }

        if (ex is MySqlException ex2)
        {
            switch (ex2.Number)
            {
                case 1062:
                    SweetAlert(controller, "帳號或編號不可重複", "帳號或編號已存在，請重新命名");
                    return;
                case 1451:
                case 1217:
                    SweetAlert(controller, "資料已產生關聯，無法刪除", "這是系統保護機制，避免誤刪重要資料");
                    return;
            }
        }

        Alert(controller, ex.ToString());
    }

    public static string HandleSqlException(this Exception ex)
    {
        if (ex is MySqlException mySqlEx)
        {
            switch (mySqlEx.Number)
            {
                case 1062:
                    return "帳號或編號已存在，請重新命名";
                case 1451:
                case 1217:
                    return "資料已產生關聯，無法刪除，這是系統保護機制，避免誤刪重要資料";
                default:
                    return ex.Message + "錯誤代碼：" + mySqlEx.Number;
            }
        }
        return ex.Message;
    }
    #endregion

    #region HttpRequest

    public static string BaseUrl(this HttpRequest httpRequest)
    {
        return $"{httpRequest.Scheme}://{httpRequest.Host}";
        //return (httpRequest.Url.Scheme + "://" + httpRequest.Url.Authority).TrimEnd('/');
    }

    #endregion
}

    //-------------------------------------------------------------------------------------------------
    // 常用範例
    //-------------------------------------------------------------------------------------------------
    // HttpContext.Session.SetString([key], [value]);
    //-------------------------------------------------------------------------------------------------
    //設定絕對快取
    //HttpRuntime.Cache.Insert(key, GetList(), null, DateTime.Now.AddSeconds(5), Cache.NoSlidingExpiration);
    //-------------------------------------------------------------------------------------------------
//}
