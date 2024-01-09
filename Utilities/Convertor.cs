using System.Text;
using System.Text.Json;
using System.Web;
//using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

//namespace dekKBS_MVC.Utility
//{
public static partial class Convertor
{
    public static T? FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static dynamic? FromJson(string json)
    {
        return JsonConvert.DeserializeObject(json);
    }

    public static async Task<T> Post<T>(string baseUri, string requestUri, object? content = null) where T : new()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUri);

            HttpContent? httpContent = null;
            if (content != null)
            {
                string json = JsonConvert.SerializeObject(content);
                httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }

            using (var response = await client.PostAsync(requestUri, httpContent))
            {
                string ret = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return FromJson<T>(ret) ?? new T();
                else
                {
                    if (ret.IsEmpty())
                        throw new Exception(response.ToString());
                    else
                        throw new Exception(ret);
                }
            }
        }
    }

    //public static void CopyDirectory(string sourceDir, string destinationDir)
    //{
    //    // Get information about the source directory
    //    var dir = new DirectoryInfo(sourceDir);

    //    // Check if the source directory exists
    //    if (!dir.Exists)
    //        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

    //    // Cache directories before we start copying
    //    DirectoryInfo[] dirs = dir.GetDirectories();

    //    // Create the destination directory
    //    Directory.CreateDirectory(destinationDir);

    //    // Get the files in the source directory and copy to the destination directory
    //    foreach (FileInfo file in dir.GetFiles())
    //    {
    //        string targetFilePath = Path.Combine(destinationDir, file.Name);
    //        file.CopyTo(targetFilePath);
    //    }

    //    // If recursive and copying subdirectories, recursively call this method
    //    foreach (DirectoryInfo subDir in dirs)
    //    {
    //        string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
    //        CopyDirectory(subDir.FullName, newDestinationDir);
    //    }
    //}

    // Extension Methods

    #region object

    public static string Text(this object? obj)
    {
        return (obj ?? "").ToString() ?? "";
    }

    public static string Left(this object? obj, int length)
    {
        if (length > 0)
        {
            string text = Text(obj);
            return text.Length > length ? text.Substring(0, length) : text;
        }
        else
            return "";
    }

    public static string Right(this object? obj, int length)
    {
        string text = Text(obj);
        return text.Length > length ? text.Substring(text.Length - length) : text;
    }

    public static bool IsEmpty(this object? obj)
    {
        return Text(obj).Trim() == "";
    }

    public static int Int(this object? obj)
    {
        return decimal.TryParse(Text(obj), out decimal ret) ? (int)ret : 0;
    }

    public static uint UInt(this object? obj)
    {
        return decimal.TryParse(Text(obj), out decimal ret) ? (uint)ret : 0;
    }

    public static string Int(this object? obj, string format)
    {
        return Int(obj).ToString(format);
    }

    public static decimal Decimal(this object? obj)
    {
        return decimal.TryParse(Text(obj), out decimal ret) ? ret : 0;
    }

    public static string Decimal(this object? obj, string format)
    {
        // #,##0.00
        // 0.##
        return Decimal(obj).ToString(format);
    }

    public static DateTime? Date(this object? obj)
    {
        return DateTime.TryParse(Text(obj), out DateTime ret) ? (DateTime?)ret : null;
    }

    public static string Date(this object? obj, string format)
    {
        // yyyy-MM-dd HH:mm:ss
        return (System.DateTime.TryParse(Text(obj), out DateTime ret) ? ret.ToString(format) : "");
    }

    public static string MD5(this object? obj)
    {
        var dataBytes = Encoding.UTF8.GetBytes(Text(obj));
        var hashBytes = System.Security.Cryptography.MD5.HashData(dataBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public static string Json(this object? obj)
    {
        if (obj is Task)
            throw new InvalidOperationException("Object of type \"Task\" cannot be converted to JSON format.");
        else
            return JsonConvert.SerializeObject(obj);
    }

    public static string JavaScriptString(this object? obj)
    {
        return HttpUtility.JavaScriptStringEncode(Text(obj));
    }

    public static string UrlString(this object? obj)
    {
        return HttpUtility.UrlEncode(Text(obj));
    }
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Encode(string data)
    {
        byte[] stringsBytes = Encoding.Unicode.GetBytes(data);
        string base64 = Convert.ToBase64String(stringsBytes);
        string base64Url = base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');

        return base64Url;

        //byte[] stringsBytes = Encoding.Unicode.GetBytes(data);
        ////string str = WebEncoders.Base64UrlEncode(stringsBytes);
        //return WebEncoders.Base64UrlEncode(stringsBytes);
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Decode(string data)
    {

        // 將 URL 安全的 Base64 編碼字符串轉換回普通的 Base64 編碼
        string base64 = data.Replace('-', '+').Replace('_', '/');

        // 根據需要補齊 Base64 編碼的等號
        int padding = 4 - base64.Length % 4;
        if (padding != 4) base64 = base64.PadRight(base64.Length + padding, '=');

        // 從 Base64 字符串解碼到 byte 陣列
        byte[] bytes = Convert.FromBase64String(base64);

        // 將 byte 陣列轉換為字符串
        return System.Text.Encoding.Unicode.GetString(bytes);

        // var StringByte = WebEncoders.Base64UrlDecode(data);
        // return System.Text.UnicodeEncoding.Unicode.GetString(StringByte); ;
    }

    #endregion
}
//}
