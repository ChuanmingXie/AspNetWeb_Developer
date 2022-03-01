namespace AspNetMvc_EFCodeFirst.Migrations
{
    using AspNetMvc_EFCodeFirst.DAL;
    using AspNetMvc_EFCodeFirst.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetMvc_EFCodeFirst.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AspNetMvc_EFCodeFirst.DAL.SchoolContext";
        }

        protected override void Seed(AspNetMvc_EFCodeFirst.DAL.SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var students = new List<Student>
            {
                new Student { FirstMidName = "张扬",   LastName = "赵",
                    EnrollmentDate = DateTime.Parse("2018-09-01") },
                new Student { FirstMidName = "莉萍", LastName = "王",
                    EnrollmentDate = DateTime.Parse("2019-09-01") },
                new Student { FirstMidName = "江西",   LastName = "古",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "浩然",    LastName = "文",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "颜",      LastName = "由",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "长泾",    LastName = "徐",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "智",    LastName = "陆",
                    EnrollmentDate = DateTime.Parse("2018-09-01") },
                new Student { FirstMidName = "文玉",     LastName = "西",
                    EnrollmentDate = DateTime.Parse("2016-08-11") }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "英语系",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "数学系", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "工程系", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "经济系",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { CourseID = 1050, Title = "化学", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="工程系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 4022, Title = "微观经济学", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="经济系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 4041, Title = "宏观经济学", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="经济系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 1045, Title = "高等数学", Credits = 4,
                    DepartmentID=departments.Single(s=>s.Name=="数学系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 3141, Title = "三角几何", Credits = 4, 
                    DepartmentID=departments.Single(s=>s.Name=="数学系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 2021, Title = "欧美诗歌", Credits = 3, 
                    DepartmentID=departments.Single(s=>s.Name=="英语系").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 2042, Title = "古典文学", Credits = 4, 
                    DepartmentID=departments.Single(s=>s.Name=="英语系").DepartmentID,
                    Instructors=new List<Instructor>()
                }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i=>i.LastName=="Fakhouri").ID,
                    Location="博学楼 217",
                },
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i=>i.LastName=="Harui").ID,
                    Location="明智楼 307",
                },
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i=>i.LastName=="Kapoor").ID,
                    Location="崇德楼 304",
                },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();
            AddOrUpdateInstructor(context, "化学", "Kapoor");
            AddOrUpdateInstructor(context, "化学", "Harui");
            AddOrUpdateInstructor(context, "微观经济学", "Zheng");
            AddOrUpdateInstructor(context, "宏观经济学", "Zheng");

            AddOrUpdateInstructor(context, "高等数学", "Fakhouri");
            AddOrUpdateInstructor(context, "三角几何", "Harui");
            AddOrUpdateInstructor(context, "欧美诗歌", "Abercrombie");
            AddOrUpdateInstructor(context, "古典文学", "Abercrombie");
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "赵").ID,
                    CourseID = courses.Single(c => c.Title == "化学" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "赵").ID,
                    CourseID = courses.Single(c => c.Title == "微观经济学" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "赵").ID,
                    CourseID = courses.Single(c => c.Title == "宏观经济学" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "王").ID,
                    CourseID = courses.Single(c => c.Title == "高等数学" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "王").ID,
                    CourseID = courses.Single(c => c.Title == "三角几何" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "王").ID,
                    CourseID = courses.Single(c => c.Title == "欧美诗歌" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "古").ID,
                    CourseID = courses.Single(c => c.Title == "化学" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "古").ID,
                    CourseID = courses.Single(c => c.Title == "微观经济学").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "文").ID,
                    CourseID = courses.Single(c => c.Title == "化学").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "由").ID,
                    CourseID = courses.Single(c => c.Title == "欧美诗歌").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "徐").ID,
                    CourseID = courses.Single(c => c.Title == "古典文学").CourseID,
                    Grade = Grade.B
                 }
            };
            foreach (var e in enrollments)
            {
                var enrollmentsInDatabase = context.Enrollments
                    .Where(s => s.Student.ID == e.StudentID && s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentsInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        private void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var course = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var instrcutor = course.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (instrcutor == null)
            {
                course.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
            }
        }
    }
}
