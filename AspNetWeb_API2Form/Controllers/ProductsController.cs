using AspNetWeb_API2Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWeb_API2Form.Controllers
{
    public class ProductsController : ApiController
    {
        /*
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */

        Product[] products = new Product[]
        {
            new Product{Id=1,Name="安踏运动服",Category="服装",Price=234.9M},
            new Product{Id=2,Name="特步运动鞋",Category="鞋子",Price=134.9M},
            new Product{Id=3,Name="鸿星尔克R4",Category="鞋子",Price=434.49M},
            new Product{Id=4,Name="棒球帽",Category="帽子",Price=24.9M},
        };

        public IEnumerable<Product> GetAllProducts() => products;

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product>GetProductsByCategory(string category)
        {
            return products.Where(p => 
            string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }
    }
}