/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.ViewModels
*项目描述:
*类 名 称:InstructorIndexData
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/1 0:01:21
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using AspNetMvc_EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFCodeFirst.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}