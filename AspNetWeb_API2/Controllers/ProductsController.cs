using AspNetWeb_API2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWeb_API2.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product{Id=1,Name="Tomato Soup",Category="Groceries",Price=1},
            new Product{Id=2,Name="Yo-yo",Category="Toys",Price=7.51M},
            new Product{Id=3,Name="Hammer",Category="Hardware",Price=16.99M},
            new Product{Id=4,Name="Shirts",Category="Cloth",Price=32.89M}
        };

        /// <summary>
        /// 获取全部产品列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        /// <summary>
        /// 更具Id获取产品列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetResult(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
