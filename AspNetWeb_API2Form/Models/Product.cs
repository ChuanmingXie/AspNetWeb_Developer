/*****************************************************************************
*项目名称:AspNetWeb_API2Form.Models
*项目描述:
*类 名 称:Product
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/14 18:08:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWeb_API2Form.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}