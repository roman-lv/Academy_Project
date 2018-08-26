using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Models;

namespace Project.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student { FirstMidName = "Ivan",   LastName = "Paliychuk",
                    EnrollmentDate = DateTime.Parse("2015-09-01") },
                new Student { FirstMidName = "Maxym", LastName = "Gudz",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "Marko",   LastName = "Hrozan",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "Evhen",    LastName = "Halych",
                    EnrollmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstMidName = "Andriy",      LastName = "Markiv",
                    EnrollmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstMidName = "Ehor",    LastName = "Kuvaldin",
                    EnrollmentDate = DateTime.Parse("2015-09-01") },
                new Student { FirstMidName = "Richard",    LastName = "Grayson",
                    EnrollmentDate = DateTime.Parse("2014-09-01") },
                new Student { FirstMidName = "Wally",     LastName = "West",
                    EnrollmentDate = DateTime.Parse("2006-09-01") }
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Oliver",     LastName = "Queen",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Barry",    LastName = "Allen",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Yuriy",   LastName = "Kaplan",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Kyrylo", LastName = "Tymoshenko",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Bohdan",   LastName = "Khmil",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Queen").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Allen").ID },
                new Department { Name = "Programming", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Tymoshenko").ID },
                new Department { Name = "Discrete Analisis",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kaplan").ID }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
        {
                new Course {CourseID = 1050, Title = "OOP",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Programming").DepartmentID
                },
                new Course {CourseID = 4022, Title = "Discrete Mathematics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Discrete Analisis").DepartmentID
                },
                new Course {CourseID = 4041, Title = "TPMS", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Discrete Analisis").DepartmentID
                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 3141, Title = "Algebra",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 2021, Title = "English",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
        };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Allen").ID,
                    Location = "Horodocka, 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Kaplan").ID,
                    Location = "Doroshenko, 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Tymoshenko").ID,
                    Location = "Lesia Ukrainka, 34" },
            };


            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
             {
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "OOP" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Tymoshenko").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "TPMS" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kaplan").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Discrete Mathematics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Khmil").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Discrete Mathematics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Tymoshenko").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Allen").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Algebra" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Allen").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "English" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Queen").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Queen").ID
                    },
             };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Grayson").ID,
                    CourseID = courses.Single(c => c.Title == "OOP" ).CourseID,
                    Grade = Grade.A
                },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Grayson").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.C
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Grayson").ID,
                    CourseID = courses.Single(c => c.Title == "Algebra" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Paliychuk").ID,
                    CourseID = courses.Single(c => c.Title == "English" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Paliychuk").ID,
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "West").ID,
                    CourseID = courses.Single(c => c.Title == "English" ).CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Paliychuk").ID,
                    CourseID = courses.Single(c => c.Title == "OOP" ).CourseID
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Paliychuk").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                    Grade = Grade.B
                    },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "West").ID,
                    CourseID = courses.Single(c => c.Title == "OOP").CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Halych").ID,
                    CourseID = courses.Single(c => c.Title == "English").CourseID,
                    Grade = Grade.B
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "West").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                    }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.ID == e.StudentID &&
                            s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
    }
    }
}