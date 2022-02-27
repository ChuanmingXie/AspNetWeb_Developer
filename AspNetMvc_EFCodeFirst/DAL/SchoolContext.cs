/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.DAL
*项目描述:
*类 名 称:SchoolContext
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/27 8:58:37
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using AspNetMvc_EFCodeFirst.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AspNetMvc_EFCodeFirst.DAL
{
    public class SchoolContext:DbContext
    {
        public SchoolContext():base("SchoolContext")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        /* 指定非复数的表单名称 */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}