using System.Collections.Generic;
using System.Collections.ObjectModel;
using AssignmentMVC.Models;

namespace AssignmentMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ApplicationDbContext context)
        {
            var c1 = new Course() { Title = "C#", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c2 = new Course() { Title = "Java", Type = "Online", Stream = "Full Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 6, 15) };
            var c3 = new Course() { Title = "SQL", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c4 = new Course() { Title = "Python", Type = "Online", Stream = "Full Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 6, 15) };
            var c5 = new Course() { Title = "Database", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c6 = new Course() { Title = "ASP.NET", Type = "Live", Stream = "Full Time", StartDate = new DateTime(2021, 4, 20), EndDate = new DateTime(2021, 9, 20) };
            var c7 = new Course() { Title = "AJAX", Type = "Live", Stream = "Full Time", StartDate = new DateTime(2021, 6, 15), EndDate = new DateTime(2021, 12, 15) };
            var c8 = new Course() { Title = "JQuery", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 4, 15) };
            var c9 = new Course() { Title = "Dapper", Type = "Live", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 5, 15) };
            var c10 = new Course() { Title = "Bootstrap", Type = "Live", Stream = "Full Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 3, 15) };

            var courses = new List<Course>() { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 };

            foreach (var course in courses)
            {
                context.Courses.AddOrUpdate(c => new { c.Title, c.Type, c.Stream, c.StartDate }, course);
            }

            var a1 = new Assignment() { Title = "Caesar Cipher", Description = "Cipher Exercise", SubDateTime = new DateTime(2021, 5, 15), OralMark = 40, TotalMark = 100, Courses = new Collection<Course>() { c1, c5 } };
            var a2 = new Assignment() { Title = "Binary to Decimal", Description = "Conversion Exercise", SubDateTime = new DateTime(2021, 5, 1), OralMark = 50, TotalMark = 100, Courses = new Collection<Course>() { c2, c5 } };
            var a3 = new Assignment() { Title = "Calculator", Description = "Calculator Exercise", SubDateTime = new DateTime(2021, 6, 17), OralMark = 30, TotalMark = 100, Courses = new Collection<Course>() { c3, c5 } };
            var a4 = new Assignment() { Title = "Minesweeper", Description = "Game Exercise", SubDateTime = new DateTime(2021, 9, 17), OralMark = 50, TotalMark = 100, Courses = new Collection<Course>() { c4, c5 } };
            var a5 = new Assignment() { Title = "Database", Description = "Database Management", SubDateTime = new DateTime(2021, 4, 7), OralMark = 30, TotalMark = 100, Courses = new Collection<Course>() { c1, c2, c3, c4, c5 } };
            var a6 = new Assignment() { Title = "Hello World", Description = "Introductory Exercise", SubDateTime = new DateTime(2021, 10, 10), OralMark = 40, TotalMark = 90, Courses = new Collection<Course>() { c1, c2, c3, c6 } };
            var a7 = new Assignment() { Title = "Cat Years", Description = "Conversion Exercise", SubDateTime = new DateTime(2021, 7, 7), OralMark = 50, TotalMark = 100, Courses = new Collection<Course>() { c1, c2, c3, c4, c5 } };
            var a8 = new Assignment() { Title = "Array Building", Description = "Array Exercise", SubDateTime = new DateTime(2021, 8, 7), OralMark = 30, TotalMark = 100, Courses = new Collection<Course>() { c6, c7, c8, c9 } };
            var a9 = new Assignment() { Title = "Create Relationships", Description = "Basic MVC Exercise", SubDateTime = new DateTime(2021, 10, 25), OralMark = 45, TotalMark = 85, Courses = new Collection<Course>() { c10, c9 } };
            var a10 = new Assignment() { Title = "Shape Designer", Description = "Graphic Design Exercise", SubDateTime = new DateTime(2021, 11, 14), OralMark = 30, TotalMark = 60, Courses = new Collection<Course>() { c5, c8, c7} };

            var assignments = new List<Assignment>() { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 };

            foreach (var assignment in assignments)
            {
                context.Assignments.AddOrUpdate(t => new { t.Title, t.Description }, assignment);
            }

            var s1 = new Student() { FirstName = "Michael", LastName = "Athanasoglou", DateOfBirth = new DateTime(1991, 2, 22), TuitionFees = 2250, Courses = new Collection<Course>() { c1, c2, c10 }, Assignments = new Collection<Assignment>(){a1, a2, a8}};
            var s2 = new Student() { FirstName = "Valia", LastName = "Magkouta", DateOfBirth = new DateTime(1991, 1, 6), TuitionFees = 2500, Courses = new Collection<Course>() { c2, c3, c9 }, Assignments = new Collection<Assignment>(){ a1, a2, a2 } };
            var s3 = new Student() { FirstName = "Makis", LastName = "Politidis", DateOfBirth = new DateTime(1990, 5, 1), TuitionFees = 2250, Courses = new Collection<Course>() { c3, c4, c8 }, Assignments = new Collection<Assignment>() { a1, a3, a5 } };
            var s4 = new Student() { FirstName = "Katerina", LastName = "Temponera", DateOfBirth = new DateTime(1995, 5, 24), TuitionFees = 0, Courses = new Collection<Course>() { c5, c1, c7 }, Assignments = new Collection<Assignment>() { a3, a4, a5 } };
            var s5 = new Student() { FirstName = "John", LastName = "Antoniou", DateOfBirth = new DateTime(1990, 2, 4), TuitionFees = 2500, Courses = new Collection<Course>() { c1, c2, c6 }, Assignments = new Collection<Assignment>() {a1, a4, a5, a6 } };
            var s6 = new Student() { FirstName = "Cathy", LastName = "Kalochristinaki", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 2250, Courses = new Collection<Course>() { c4, c5 }, Assignments = new Collection<Assignment>() { a4, a5, a6 } };
            var s7 = new Student() { FirstName = "Vasilis", LastName = "Kolias", DateOfBirth = new DateTime(1990, 5, 24), TuitionFees = 0, Courses = new Collection<Course>() { c2, c3, c6, c8 }, Assignments = new Collection<Assignment>() { a2, a1, a3 } };
            var s8 = new Student() { FirstName = "Lampros", LastName = "Larisaios", DateOfBirth = new DateTime(1988, 7, 15), TuitionFees = 2250, Courses = new Collection<Course>() { c4, c5, c9, c10 }, Assignments = new Collection<Assignment>() { a4, a5, a6 } };
            var s9 = new Student() { FirstName = "Odysseas", LastName = "Argyris", DateOfBirth = new DateTime(1992, 1, 19), TuitionFees = 2500, Courses = new Collection<Course>() { c5,c7,c8 }, Assignments = new Collection<Assignment>() { a5, a7, a8 } };
            var s10 = new Student() { FirstName = "John", LastName = "Antoniou", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c4, c5,c7 }, Assignments = new Collection<Assignment>() { a4, a5, a6, a9, a10 } };

            var students = new List<Student>() { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 };

            foreach (var student in students)
            {
                context.Students.AddOrUpdate(s => new { s.FirstName, s.LastName, s.DateOfBirth }, student);
            }

            var t1 = new Trainer() { FirstName = "Konstantinos", LastName = "Takakis", Subject = "Programming", Courses = new Collection<Course>() { c1, c2 } };
            var t2 = new Trainer() { FirstName = "Sakis", LastName = "Siklarlis", Subject = "Database", Courses = new Collection<Course>() { c2, c3 } };
            var t3 = new Trainer() { FirstName = "Ektoras", LastName = "Gkatsos", Subject = "Programming", Courses = new Collection<Course>() { c1, c5 } };
            var t4 = new Trainer() { FirstName = "Seleukos", LastName = "Nikator", Subject = "Database", Courses = new Collection<Course>() { c1, c2, c5 } };
            var t5 = new Trainer() { FirstName = "Maria", LastName = "Antoniou", Subject = "Programming", Courses = new Collection<Course>() { c4, c3, c5 } };
            var t6 = new Trainer() { FirstName = "Sergio", LastName = "Marquina", Subject = "Heists", Courses = new Collection<Course>() { c6, c8, c10 } };
            var t7 = new Trainer() { FirstName = "Walter", LastName = "White", Subject = "Chemistry", Courses = new Collection<Course>() { c7, c8, c9, c10 } };
            var t8 = new Trainer() { FirstName = "Marcus", LastName = "Aurelius", Subject = "Philosophy", Courses = new Collection<Course>() { c4, c7, c9, c10 } };
            var t9 = new Trainer() { FirstName = "Konstantinos", LastName = "Palaiologos", Subject = "City Defense", Courses = new Collection<Course>() { c3, c2, c7, c9 } };
            var t10 = new Trainer() { FirstName = "Darth", LastName = "Vader", Subject = "Jedi Hunting", Courses = new Collection<Course>() { c5, c6, c9 } };

            var trainers = new List<Trainer>() { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10 };

            foreach (var trainer in trainers)
            {
                context.Trainers.AddOrUpdate(t => new { t.FirstName, t.LastName }, trainer);
            }

            context.SaveChanges();
        }
    }
}
