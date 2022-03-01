/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.ViewModels
*项目描述:
*类 名 称:AssignedCourseData
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/1 13:34:19
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFCodeFirst.ViewModels
{
    public class AssignedCourseData
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}