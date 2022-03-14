/*
    ȡ��ע���������ݣ�ΪPageResult<T>�ṩʾ����
    ��������ӿ�����Microsoft.AspNet.WebApi.OData��������Ŀ��
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
    /// ʹ�ô�����Զ������ҳ�档
    /// ���磬�����������Զ���<see cref="System.Web.Http.Description.IDocumentationProvider"/> �����ṩ�ĵ�
    /// �����������ṩ����/��Ӧ��������
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
            //ȡ��ע������������ʹ��XML�ĵ��ļ��е��ĵ���
            //config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            /*
             * ȡ�����������ݵ�ע�ͣ�����sample string������������string��Ϊ��������򷵻����͵Ĳ�����ʾ����
             * ���⣬�ַ������齫����IEnumerable<string>ʾ�����󽫱����л�Ϊ��ͬ��ý�������ɿ��õĸ�ʽ�������ʽ����
             */
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {typeof(string), "sample string"},
            //    {typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}}
            //});

            /*
             * ��չ�������ݣ�Ϊδ�Զ���������ͣ�ȱ���޲������캯�������ͣ�������ϲ��ʹ�÷�Ĭ������ֵ�������ṩ������
             * ��һ���ṩ��һ���󱸷�������Ϊ�Զ�����ʧ�ܣ�GeneratePageResultֻ����һ�����͡�
             */
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

            /*
             * ��չ�������ݣ���Ԥ�����ֱ������֧��ý������в�����ʾ�����ͣ�����������������򷵻����͡�
             * ���¼��б�����ʾ���������ݡ�BsonMediaTypeFormatter��������ã����������л�TextSample����
             */
            config.SetSampleForMediaType(
                new TextSample("Binary JSON content. See http://bsonspec.org for details."),
                new MediaTypeHeaderValue("application/bson"));

            /*
             * ȡ��ע���������ݣ�����[0]=foo&[1]=bar��ֱ����������֧�ֱ�URL�����ʽ����IEnumerable<string>��Ϊ��������򷵻����͵Ĳ�����ʾ����
             */
            //config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-urlencoded"), typeof(IEnumerable<string>));

            //// ȡ��ע���������ݣ�����1234��ֱ��������Ϊ��Values���Ŀ�������ý�����͡�text/plain������Ϊ��Put���Ĳ���������ʾ����
            //config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");

            /*
             * ȡ��ע���������ݣ�����./images/aspNetHome.png���ϵ�ͼ��ֱ������ý�����͡�image/png������Ӧʾ��
             * ����Ϊ��Values���Ŀ���������Ϊ��Get���Ҳ���Ϊ��id���Ĳ����ϡ�
             */
            //config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");

            /*
             * ��������ҪObjectContent<string>��HttpRequestMessageʱ����ȡ��ע�����������Ը���ʾ������
             * ��ʾ���������ɣ�������Ϊ��Values���Ŀ���������Ϊ��Get���Ĳ������ַ�����Ϊ�������һ����
             */
            //config.SetActualRequestType(typeof(string), "Values", "Get");

            /*
             * ���������ش���0ObjectContent<string>��HttpResponseMessageʱ����ȡ��ע�����������Ը���ʾ����Ӧ��
             * ������ʾ����������Ϊ��Values���Ŀ���������Ϊ��Post���Ĳ��������ַ���һ����
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