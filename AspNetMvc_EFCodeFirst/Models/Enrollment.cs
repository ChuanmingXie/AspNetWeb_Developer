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
using System.ComponentModel.DataAnnotations;
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
        [Display(Name ="入学编号")]
        public int EnrollmentID { get; set; }

        [Display(Name ="课程代码")]
        public int CourseID { get; set; }

        [Display(Name ="学生证号")]
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText ="暂无级别")]
        [Display(Name ="测试等级")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { set; get; }
    }
}