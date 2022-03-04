/*****************************************************************************
*项目名称:AspNetMvc_WeChat.Models
*项目描述:
*类 名 称:Student
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/3 22:13:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMvc_WeChat.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    [DisplayName("学生")]
    public class Student
    {
        [Display(Name ="学生证号")]
        public int StudentID { get; set; }

        [Display(Name="姓名")]
        public string StudentName { get; set; }

        public Gender Gender { get; set; }

        [Display(Name ="年龄")]
        public int Age { get; set; }

        [Display(Name ="是否新生入学")]
        public bool isNewlyEnrolled { get; set; }

        [Display(Name ="密码")]
        public string Password { get; set; }

        [Display(Name ="生日")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        [Display(Name ="简介")]
        [StringLength(200)]
        public string Description { get; set; }
    }
}