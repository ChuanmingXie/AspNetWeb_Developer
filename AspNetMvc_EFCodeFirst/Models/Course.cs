/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Models
*项目描述:
*类 名 称:Course
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/27 8:55:32
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvc_EFCodeFirst.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="课程代码")]
        public int CourseID { get; set; }

        [StringLength(50,MinimumLength =2)]
        [Display(Name ="课程名称")]
        public string Title { get; set; }

        [Range(0,5),Display(Name ="学分")]
        public int Credits { get; set; }

        [Display(Name ="院系代码")]
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}