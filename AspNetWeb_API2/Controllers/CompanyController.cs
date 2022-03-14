using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWeb_API2.Controllers
{
    /// <summary>
    /// 供应商管理
    /// </summary>
    public class CompanyController : ApiController
    {
        // GET: api/Company
        /// <summary>
        /// 获取全部供应商列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Company/5
        /// <summary>
        /// 通过Id获取供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Company
        /// <summary>
        /// 创建新的供应商
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Company/5
        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Company/5
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
        }
    }
}
