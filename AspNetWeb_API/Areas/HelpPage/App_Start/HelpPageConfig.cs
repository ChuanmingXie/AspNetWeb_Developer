/*
    取消注释以下内容，为PageResult<T>提供示例。
    还必须添加开发包Microsoft.AspNet.WebApi.OData到您的项目中
 */
////#define Handle_PageResultOfT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
#if Handle_PageResultOfT
using System.Web.Http.OData;
#endif

namespace AspNetWeb_API.Areas.HelpPage
{
    /// <summary>
    /// 使用此类可自定义帮助页面。
    /// 例如，您可以设置自定义<see cref="System.Web.Http.Description.IDocumentationProvider"/> 用来提供文档
    /// 或者您可以提供请求/响应的样本。
    /// </summary>
    public static class HelpPageConfig
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "AspNetWeb_API.Areas.HelpPage.TextSample.#ctor(System.String)",
            Justification = "End users may choose to merge this string with existing localized resources.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
            MessageId = "bsonspec",
            Justification = "Part of a URI.")]
        public static void Register(HttpConfiguration config)
        {
            //取消注释以下内容以使用XML文档文件中的文档。
            //config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            /*
             * 取消对以下内容的注释，将“sample string”用作所有以string作为主体参数或返回类型的操作的示例。
             * 此外，字符串数组将用于IEnumerable<string>示例对象将被序列化为不同的媒体类型由可用的格式化程序格式化。
             */
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {typeof(string), "sample string"},
            //    {typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}}
            //});

            /*
             * 扩展以下内容，为未自动处理的类型（缺少无参数构造函数的类型）或您更喜欢使用非默认属性值的类型提供工厂。
             * 下一行提供了一个后备方案，因为自动处理将失败，GeneratePageResult只处理一种类型。
             */
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

            /*
             * 扩展以下内容，将预设对象直接用作支持媒体的所有操作的示例类型，而不考虑主体参数或返回类型。
             * 以下几行避免显示二进制内容。BsonMediaTypeFormatter（如果可用）不用于序列化TextSample对象。
             */
            config.SetSampleForMediaType(
                new TextSample("Binary JSON content. See http://bsonspec.org for details."),
                new MediaTypeHeaderValue("application/bson"));

            /*
             * 取消注释以下内容，将“[0]=foo&[1]=bar”直接用作所有支持表单URL编码格式并将IEnumerable<string>作为主体参数或返回类型的操作的示例。
             */
            //config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-urlencoded"), typeof(IEnumerable<string>));

            //// 取消注释以下内容，将“1234”直接用作名为“Values”的控制器上媒体类型“text/plain”和名为“Put”的操作的请求示例。
            //config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");

            /*
             * 取消注释以下内容，将“./images/aspNetHome.png”上的图像直接用作媒体类型“image/png”的响应示例
             * 在名为“Values”的控制器和名为“Get”且参数为“id”的操作上。
             */
            //config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");

            /*
             * 当操作需要ObjectContent<string>的HttpRequestMessage时，请取消注释以下内容以更正示例请求。
             * 该示例将被生成，就像名为“Values”的控制器和名为“Get”的操作将字符串作为主体参数一样。
             */
            //config.SetActualRequestType(typeof(string), "Values", "Get");

            /*
             * 当操作返回带有0ObjectContent<string>的HttpResponseMessage时，请取消注释以下内容以更正示例响应。
             * 将生成示例，就像名为“Values”的控制器和名为“Post”的操作返回字符串一样。
             */
            //config.SetActualResponseType(typeof(string), "Values", "Post");
        }

#if Handle_PageResultOfT
        private static object GeneratePageResult(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            if (type.IsGenericType)
            {
                Type openGenericType = type.GetGenericTypeDefinition();
                if (openGenericType == typeof(PageResult<>))
                {
                    // Get the T in PageResult<T>
                    Type[] typeParameters = type.GetGenericArguments();
                    Debug.Assert(typeParameters.Length == 1);

                    // Create an enumeration to pass as the first parameter to the PageResult<T> constuctor
                    Type itemsType = typeof(List<>).MakeGenericType(typeParameters);
                    object items = sampleGenerator.GetSampleObject(itemsType);

                    // Fill in the other information needed to invoke the PageResult<T> constuctor
                    Type[] parameterTypes = new Type[] { itemsType, typeof(Uri), typeof(long?), };
                    object[] parameters = new object[] { items, null, (long)ObjectGenerator.DefaultCollectionSize, };

                    // Call PageResult(IEnumerable<T> items, Uri nextPageLink, long? count) constructor
                    ConstructorInfo constructor = type.GetConstructor(parameterTypes);
                    return constructor.Invoke(parameters);
                }
            }

            return null;
        }
#endif
    }
}