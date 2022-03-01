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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Person> People { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* 指定非复数的表单名称 */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /* 语句配置多对多联接表 */
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID").MapRightKey("InstructorID").ToTable("CourseInstructor"));
            /* 语句配置多对多联接表 -Fluent API */
            //modelBuilder.Entity<Instructor>().HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);

            /*指示实体框架在实体上 Department 使用存储过程执行插入、更新和删除操作*/
            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}