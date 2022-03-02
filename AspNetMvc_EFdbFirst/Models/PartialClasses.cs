/*****************************************************************************
*项目名称:AspNetMvc_EFdbFirst.Models
*项目描述:
*类 名 称:PartialClasses
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/2 22:27:38
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace AspNetMvc_EFdbFirst.Models
{
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        [Display(Name = "姓名")]
        public string FullName { get { return FirstName + "·" + MiddleName + "·" + LastName; } }
    }
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment
    {

    }
}