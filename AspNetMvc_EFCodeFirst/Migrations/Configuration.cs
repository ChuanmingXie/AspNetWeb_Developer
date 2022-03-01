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
                new Student { FirstMidName = "����",   LastName = "��",
                    EnrollmentDate = DateTime.Parse("2018-09-01") },
                new Student { FirstMidName = "��Ƽ", LastName = "��",
                    EnrollmentDate = DateTime.Parse("2019-09-01") },
                new Student { FirstMidName = "����",   LastName = "��",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "��Ȼ",    LastName = "��",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "��",      LastName = "��",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "����",    LastName = "��",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "��",    LastName = "½",
                    EnrollmentDate = DateTime.Parse("2018-09-01") },
                new Student { FirstMidName = "����",     LastName = "��",
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
                new Department { Name = "Ӣ��ϵ",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "��ѧϵ", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "����ϵ", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "����ϵ",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { CourseID = 1050, Title = "��ѧ", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="����ϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 4022, Title = "΢�۾���ѧ", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="����ϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 4041, Title = "��۾���ѧ", Credits = 3,
                    DepartmentID=departments.Single(s=>s.Name=="����ϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 1045, Title = "�ߵ���ѧ", Credits = 4,
                    DepartmentID=departments.Single(s=>s.Name=="��ѧϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 3141, Title = "���Ǽ���", Credits = 4, 
                    DepartmentID=departments.Single(s=>s.Name=="��ѧϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 2021, Title = "ŷ��ʫ��", Credits = 3, 
                    DepartmentID=departments.Single(s=>s.Name=="Ӣ��ϵ").DepartmentID,
                    Instructors=new List<Instructor>()
                },
                new Course { CourseID = 2042, Title = "�ŵ���ѧ", Credits = 4, 
                    DepartmentID=departments.Single(s=>s.Name=="Ӣ��ϵ").DepartmentID,
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
                    Location="��ѧ¥ 217",
                },
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i=>i.LastName=="Harui").ID,
                    Location="����¥ 307",
                },
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i=>i.LastName=="Kapoor").ID,
                    Location="���¥ 304",
                },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();
            AddOrUpdateInstructor(context, "��ѧ", "Kapoor");
            AddOrUpdateInstructor(context, "��ѧ", "Harui");
            AddOrUpdateInstructor(context, "΢�۾���ѧ", "Zheng");
            AddOrUpdateInstructor(context, "��۾���ѧ", "Zheng");

            AddOrUpdateInstructor(context, "�ߵ���ѧ", "Fakhouri");
            AddOrUpdateInstructor(context, "���Ǽ���", "Harui");
            AddOrUpdateInstructor(context, "ŷ��ʫ��", "Abercrombie");
            AddOrUpdateInstructor(context, "�ŵ���ѧ", "Abercrombie");
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "��ѧ" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "΢�۾���ѧ" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "��۾���ѧ" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "�ߵ���ѧ" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "���Ǽ���" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "ŷ��ʫ��" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "��ѧ" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "΢�۾���ѧ").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "��ѧ").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "ŷ��ʫ��").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "��").ID,
                    CourseID = courses.Single(c => c.Title == "�ŵ���ѧ").CourseID,
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
