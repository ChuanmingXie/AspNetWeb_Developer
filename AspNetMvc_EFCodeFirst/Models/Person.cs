/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Models
*项目描述:
*类 名 称:Person
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/1 21:22:58
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AspNetMvc_EFCodeFirst.Models
{
    public abstract class Person
    {
        public int ID { get; set; }

        [Required, StringLength(50), Display(Name = "姓氏")]
        public string LastName { get; set; }

        [Required, Column("FirstName"), Display(Name = "名字")]
        [StringLength(50, ErrorMessage = "名字过长,不能超过50个字符.")]
        public string FirstMidName { get; set; }

        [Display(Name = "姓名")]
        public string FullName
        {
            get
            {
                return Regex.IsMatch(FirstMidName, @"^[a-zA-Z'\s-]*$") ?
                  FirstMidName + "·" + LastName :LastName + FirstMidName;
            }
        }
    }
}