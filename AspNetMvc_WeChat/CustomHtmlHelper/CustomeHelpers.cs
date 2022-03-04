/*****************************************************************************
*项目名称:AspNetMvc_WeChat.CustomHtmlHelper
*项目描述:
*类 名 称:CustomeHelpers
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/4 15:16:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AspNetMvc_WeChat.CustomHtmlHelper
{
    public static class CustomeHelpers
    {
        /// <summary>
        /// 提交按钮控件
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="buttonText"></param>
        /// <returns></returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string buttonText)
        {
            string str = "<input type=\"submit\" value=\"" + buttonText + "\" />";
            return MvcHtmlString.Create(str);
        }
        /// <summary>
        /// 只读的强类型TextBox Helper控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxReadOnlyFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper
            , Expression<Func<TModel, TValue>> expression
            , bool isReadonly)
        {
            MvcHtmlString html;
            if (isReadonly)
            {
                html = InputExtensions.TextBoxFor(htmlHelper, expression
                    , new { @class = "readOnly", @readOnly = "read-only" });
            }
            else
            {
                html = InputExtensions.TextBoxFor(htmlHelper, expression);
            }
            return html;
        }
    }
}