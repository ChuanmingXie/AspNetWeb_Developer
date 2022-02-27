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
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvc_EFCodeFirst.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}