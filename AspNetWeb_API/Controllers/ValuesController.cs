﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWeb_API.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        /// 从服务器中获取全部数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
        }
    }
}
