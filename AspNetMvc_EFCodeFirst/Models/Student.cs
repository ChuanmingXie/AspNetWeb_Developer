/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Models
*项目描述:
*类 名 称:Student
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/27 8:43:46
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvc_EFCodeFirst.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 1)]
        [Display(Name ="姓氏")]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="名字")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        [StringLength(50,ErrorMessage ="名字过长,不能超过50个字符.")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="入学时间")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name ="姓名")]
        public string FullName
        {
            get { return LastName + FirstMidName; }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}