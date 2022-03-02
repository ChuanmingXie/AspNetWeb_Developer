/*****************************************************************************
*项目名称:AspNetMvc_EFdbFirst.Models
*项目描述:
*类 名 称:Metadata
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/2 22:23:47
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFdbFirst.Models
{
    public class StudentMetadata
    {
        [StringLength(50)]
        [Display(Name = "姓氏")]
        public string LastName;

        [StringLength(50)]
        [Display(Name = "名字")]
        public string FirstName;

        [StringLength(50)]
        [Display(Name = "中间名")]
        public string MiddleName;

        [Display(Name = "入学日期")]
        public Nullable<System.DateTime> EnrollmentDate;
    }

    public class EnrollmentMetadata
    {
        [Display(Name ="分数"),Range(0,4)]
        public Nullable<decimal> Grade;
    }
}