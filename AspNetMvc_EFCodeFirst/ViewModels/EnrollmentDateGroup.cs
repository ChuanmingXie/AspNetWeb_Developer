/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.ViewModels
*项目描述:
*类 名 称:EnrollmentDateGroup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/27 23:05:07
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

namespace AspNetMvc_EFCodeFirst.ViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        [Display(Name ="入学时间")]
        public DateTime? EnrollmentDate { get; set; }
        
        [Display(Name ="入学人数")]
        public int StudentCount { get; set; }
    }
}