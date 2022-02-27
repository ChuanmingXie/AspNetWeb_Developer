/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Models
*项目描述:
*类 名 称:Enrollment
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/27 8:52:29
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFCodeFirst.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { set; get; }
    }
}