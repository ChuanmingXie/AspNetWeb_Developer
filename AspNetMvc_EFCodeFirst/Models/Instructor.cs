/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Models
*项目描述:
*类 名 称:Instructor
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 22:31:26
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
    public class Instructor:Person
    {
        //public int ID { get; set; }

        //[Display(Name ="姓氏"),StringLength(50,MinimumLength =2)]
        //public string LastName { get; set; }

        //[Column("FirstName")]
        //[Display(Name ="名称"),StringLength(50,MinimumLength =2)]
        //public string FirstMidName { get; set; }

        [DataType(DataType.Date),Display(Name ="入职时间")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime HireDate { get; set; }

        //[Display(Name ="姓名")]
        //public string FullName
        //{
        //    get { return FirstMidName + "·" + LastName; }
        //}

        public virtual ICollection<Course> Courses { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}