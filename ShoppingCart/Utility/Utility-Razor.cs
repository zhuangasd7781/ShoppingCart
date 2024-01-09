//namespace dekKBS_MVC.Utility
//{
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

public static partial class Utility
{
    #region ViewContext

    public static string ActionId(this ViewContext viewContext)
    {
        return viewContext.RouteData.Values["actionId"].Text();
    }
    public static string ControllerName(this ViewContext viewContext)
    {
        return viewContext.RouteData.Values["controller"].Text();
    }
    #endregion
    #region HtmlHelper

    public static HtmlString Button(this IHtmlHelper helper, string text, string? action, string css = "")
    {
        string[] arg = new string[] { "", "", "" }; // 類型、樣式、圖示

        // List -----------------------------------------------------------------------------------
        if (text.StartsWith("查詢"))
            arg = new string[] { "", "btn-default", "fa-search" };
        else if (text.StartsWith("新增"))
            arg = new string[] { "", "btn-secondary", "fa-edit" };
        else if (text.StartsWith("詳細"))
            arg = new string[] { "", "btn-secondary btn-xs", "fa-file" };
        else if (text.StartsWith("刪除"))
            arg = new string[] { "button", "btn-danger btn-xs", "fa-trash" };

        // Edit -----------------------------------------------------------------------------------
        else if (text.StartsWith("儲存"))
            arg = new string[] { "upload", "btn-primary", "fa-save" };
        //else if (text.StartsWith("重設"))
        //    arg = new string[] { "submit", "btn-danger btn-sm", "fa-rotate-left" };
        else if (text.StartsWith("返回"))
            arg = new string[] { "redirect", "btn-default", "fa-reply" };
        //else if (text.StartsWith("上傳"))
        //    arg = new string[] { "upload", "btn-primary", "fa-upload" };

        //-----------------------------------------------------------------------------------------
        string type = "type='submit'";
        string formenctype = "";
        string attr = action != null && action.StartsWith(":") ?
            $":formaction='{action.Substring(1)}'" :
            $"formaction='{action}'";

        if (arg[0] == "upload")
        {
            formenctype = "formenctype='multipart/form-data'";
        }
        else if (arg[0] == "button")
        {
            type = "type='button'";
            attr = action != null && action.StartsWith(":") ?
                $"v-on:click='{action.Substring(1)}'" :
                $"onclick='{action}'";
        }
        else if (arg[0] == "open")
        {
            type = "type='button'";
            attr = action != null && action.StartsWith(":") ?
                $"v-on:click='{action.Substring(1)}'" :
                $"onclick='window.open(\"{action}\")'";
        }
        else if (arg[0] == "redirect")
        {
            type = "type='button'";
            attr = action != null && action.StartsWith(":") ?
                $"v-on:click='{action.Substring(1)}'" :
                $"onclick='window.location.href=\"{action}\"'";
        }
        else if (arg[0] == "modal")
        {
            type = "type='button'";
            attr = $"data-toggle='modal' data-target='{action}'";
        }

        //-----------------------------------------------------------------------------------------
        // 顏色：btn-secondary(灰)、btn-dark(深灰)、btn-primary(藍)、btn-success(綠)、btn-info(藍綠)、btn-warning(黃)、btn-danger(紅)
        // 尺寸：btn-xs、btn-sm、btn-lgstring
        string html = $"<button {type} {formenctype} {attr} class='btn {arg[1]} {css}'><i class='fa {arg[2]}'></i>&nbsp;&nbsp;<span class='bold'>{text}</span></button>";
        return new HtmlString(html);
    }

    public static HtmlString RenderScript(this IHtmlHelper helper)
    {
        StringBuilder sb = new StringBuilder();
        string alert = helper.ViewContext.TempData["Utility.Alert"].Text();
        string[]? sweetAlert = helper.ViewContext.TempData["Utility.SweetAlert"] as string[];

        if (!alert.IsEmpty())
            sb.Append($"<script>alert('{alert.JavaScriptString()}');</script>");

        if (sweetAlert != null && sweetAlert.Length >= 2)
            sb.Append($"<script>sweetAlert('{sweetAlert[0].JavaScriptString()}','{sweetAlert[1].JavaScriptString()}');</script>");

        return new HtmlString(sb.ToString());
    }

    //public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text, string action)
    //{
    //    // 顏色：btn-white(白)、btn-primary(綠)、btn-success(藍)、btn-info(藍綠)、btn-warning(黃)、 btn-danger(紅)
    //    // 尺寸：btn-xs、btn-sm、btn-lg

    //    string[] arg;

    //    // Toolbar
    //    if (text.StartsWith("查詢"))
    //        arg = new string[] { "btn-primary btn-sm", "fa-search" };
    //    else if (text.StartsWith("新增"))
    //        arg = new string[] { "btn-success btn-sm", "fa-edit" };
    //    else if (text.StartsWith("加入"))
    //        arg = new string[] { "btn-success btn-sm", "fa-plus-square" };
    //    else if (text.StartsWith("匯出"))
    //        arg = new string[] { "btn-success btn-sm", "fa-cloud-download" };
    //    // List
    //    else if (text.StartsWith("詳細"))
    //        arg = new string[] { "btn-primary btn-xs", "fa-file-text-o" };
    //    else if (text.StartsWith("複製"))
    //        arg = new string[] { "btn-success btn-xs", "fa-copy" };
    //    else if (text.StartsWith("發送"))
    //        arg = new string[] { "btn-success btn-xs", "fa-envelope-open-o" };
    //    // Edit 
    //    else if (text.StartsWith("儲存"))
    //        arg = new string[] { "btn-primary btn-sm", "fa-save" };
    //    else if (text.StartsWith("送出") || text.StartsWith("確定送審"))
    //        arg = new string[] { "btn-danger btn-sm", "fa-send-o" };
    //    else if (text.StartsWith("審核"))
    //        arg = new string[] { "btn-primary btn-sm", "fa-check-square-o" };
    //    else if (text.StartsWith("返回"))
    //        arg = new string[] { "btn-white btn-sm", "fa-reply" };
    //    else if (text.StartsWith("修改歷程"))
    //        arg = new string[] { "btn-white btn-sm", "fa-history" };
    //    // 案場
    //    else if (text.StartsWith("案場"))
    //        arg = new string[] { "btn-primary btn-xs", "fa-windows" };
    //    else
    //        arg = new string[] {"","" };

    //    string formaction = (action.StartsWith("v-bind:") ? "v-bind:" : "") + "formaction='" + action.Replace("v-bind:", "") + "'";

    //    string html = "<button type='submit' " + formaction + " class='btn " + arg[0] + "'><i class='fa " + arg[1] + "'></i>&nbsp;&nbsp;<span class='bold'>" + text + "</span></button>";
    //    return new MvcHtmlString(html);
    //}

    //public static MvcHtmlString UploadButton(this HtmlHelper helper, string text, string action)
    //{
    //    // 顏色：btn-white(白)、btn-primary(綠)、btn-success(藍)、btn-info(藍綠)、btn-warning(黃)、 btn-danger(紅)
    //    // 尺寸：btn-xs、btn-sm、btn-lg

    //    string[] arg;

    //    if (text.StartsWith("上傳"))
    //        arg = new string[] { "btn-primary", "fa-upload" };
    //    else if (text.StartsWith("儲存"))
    //        arg = new string[] { "btn-primary btn-sm", "fa-save" };
    //    else if (text.StartsWith("送出") || text.StartsWith("確定送審"))
    //        arg = new string[] { "btn-danger btn-sm", "fa-send-o" };
    //    else
    //        arg = new string[] { "", "" };

    //    string html = "<button type='submit' formenctype='multipart/form-data' formaction='" + action + "' class='btn " + arg[0] + "'><i class='fa " + arg[1] + "'></i>&nbsp;&nbsp;<span class='bold'>" + text + "</span></button>";
    //    return new MvcHtmlString(html);
    //}

    //public static MvcHtmlString ConfirmButton(this HtmlHelper helper, string text, string action)
    //{
    //    // 顏色：btn-white(白)、btn-primary(綠)、btn-success(藍)、btn-info(藍綠)、btn-warning(黃)、 btn-danger(紅)
    //    // 尺寸：btn-xs、btn-sm、btn-lg

    //    string[] arg;

    //    if (text.StartsWith("刪除"))
    //        arg = new string[] { "確定要刪除嗎？", "btn-danger btn-xs", "fa-trash"};
    //    else if (text.StartsWith("移除"))
    //        arg = new string[] { "確定要移除嗎？", "btn-danger btn-xs", "fa-minus-square" };
    //    else
    //        arg = new string[] { "", "", "" };

    //    string html = "<button type='button' onclick=\"sweetConfirm('" + arg[0].JavaScriptString() + "','','" + action + "');\" class='btn " + arg[1] + "'><i class='fa " + arg[2] + "'></i>&nbsp;&nbsp;<span class='bold'>" + text + "</span></button>";
    //    return new MvcHtmlString(html);
    //}

    //public static MvcHtmlString OpenButton(this HtmlHelper helper, string text, string url)
    //{
    //    // 顏色：btn-white(白)、btn-primary(綠)、btn-success(藍)、btn-info(藍綠)、btn-warning(黃)、 btn-danger(紅)
    //    // 尺寸：btn-xs、btn-sm、btn-lg

    //    string[] arg;

    //    if (text.StartsWith("開啟"))
    //        arg = new string[] { "btn-success btn-sm", "fa-edit" };
    //    else if (text.StartsWith("下載"))
    //        arg = new string[] { "btn-success btn-sm", "fa-download" };
    //    else if (text.StartsWith("新增"))
    //        arg = new string[] { "btn-success btn-sm", "fa-edit" };
    //    else
    //        arg = new string[] { "", "" };

    //    string html = "<button type='button' onclick=\"window.open('" + url + "');\" class='btn " + arg[0] + "'><i class='fa " + arg[1] + "'></i>&nbsp;&nbsp;<span class='bold'>" + text + "</span></button>";
    //    return new MvcHtmlString(html);
    //}

    //public static MvcHtmlString ModalButton(this HtmlHelper helper, string text)
    //{
    //    // 顏色：btn-white(白)、btn-primary(綠)、btn-success(藍)、btn-info(藍綠)、btn-warning(黃)、 btn-danger(紅)
    //    // 尺寸：btn-xs、btn-sm、btn-lg

    //    string[] arg;

    //    if (text.StartsWith("匯入"))
    //        arg = new string[] { "#ImportModal", "btn-success btn-sm", "fa-cloud-upload" };
    //    else
    //        arg = new string[] { "", "", "" };

    //    string html = "<button type='button' data-toggle='modal' data-target='" + arg[0] + "' class='btn " + arg[1] + "'><i class='fa " + arg[2] + "'></i>&nbsp;&nbsp;<span class='bold'>" + text + "</span></button>";
    //    return new MvcHtmlString(html);
    //}

    //public static MvcHtmlString SelectList(this HtmlHelper helper, string name, string css, string style, string required, bool autoPostBack = false)
    //{
    //    var select = helper.ViewData["Utility.HtmlSelect-" + name] as List<SelectListItem> ?? new List<SelectListItem>();
    //    var onchange = autoPostBack ? "this.form.submit();" : "";

    //    return SelectExtensions.DropDownList(helper, name, select, new { @class = "form-control " + css, style, required, onchange });
    //}

    //public static MvcHtmlString SelectList(this HtmlHelper helper, string name, string css, string style, bool autoPostBack = false)
    //{
    //    return SelectList(helper, name, css, style, "", autoPostBack);
    //}

    //public static MvcHtmlString SelectList(this HtmlHelper helper, string name, bool autoPostBack = false)
    //{
    //    return SelectList(helper, name, "", "", autoPostBack);
    //}

    //public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string name)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    var list = helper.ViewData["Utility.HtmlSelect-" + name] as List<SelectListItem> ?? new List<SelectListItem>();
    //    foreach (var item in list)
    //    {
    //        sb.Append("<div class='i-checks'><label><input type='radio' name='" + name + "' value='" + item.Value + "' " + (item.Selected ? "checked" : "") + "/><i></i> " + item.Text + " </label></div>\n");
    //    }
    //    return new MvcHtmlString(sb.ToString());
    //}

    //public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    var list = helper.ViewData["Utility.HtmlSelect-" + name] as List<SelectListItem> ?? new List<SelectListItem>();
    //    foreach (var item in list)
    //    {
    //        sb.Append("<div class='i-checks'><label><input type='checkbox' name='" + name + "' value='" + item.Value + "' " + (item.Selected ? "checked" : "") + "/><i></i> " + item.Text + " </label></div>");
    //    }
    //    return new MvcHtmlString(sb.ToString());
    //}
    #endregion
}
//}
