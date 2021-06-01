using System.Collections.Generic;
using System.Collections.ObjectModel;
using AssignmentMVC.Models;

namespace AssignmentMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AssignmentMVC.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AssignmentMVC.DAL.ApplicationDbContext context)
        {
            var c1 = new Course() { Title = "C#", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c2 = new Course() { Title = "Java", Type = "Online", Stream = "Full Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c3 = new Course() { Title = "JavaScript", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c4 = new Course() { Title = "Python", Type = "Online", Stream = "Full Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };
            var c5 = new Course() { Title = "Database", Type = "Online", Stream = "Part Time", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 9, 15) };

            var courses = new List<Course>() { c1, c2, c3, c4, c5 };

            foreach (var course in courses)
            {
                context.Courses.AddOrUpdate(c => new { c.Title, c.Type, c.Stream, c.StartDate }, course);
            }

            var a1 = new Assignment() { Title = "ProjectC#", Description = "Individual", SubDateTime = new DateTime(2021, 5, 15), OralMark = 40, TotalMark = 100, Courses = new Collection<Course>() { c1, c5 } };
            var a2 = new Assignment() { Title = "ProjectJava", Description = "Individual", SubDateTime = new DateTime(2021, 5, 1), OralMark = 50, TotalMark = 100, Courses = new Collection<Course>() { c2, c5 } };
            var a3 = new Assignment() { Title = "ProjectJavaScript", Description = "Individual", SubDateTime = new DateTime(2021, 6, 17), OralMark = 30, TotalMark = 100, Courses = new Collection<Course>() { c3, c5 } };
            var a4 = new Assignment() { Title = "ProjectPython", Description = "Individual", SubDateTime = new DateTime(2021, 9, 17), OralMark = 50, TotalMark = 100, Courses = new Collection<Course>() { c4, c5 } };
            var a5 = new Assignment() { Title = "Database", Description = "Group", SubDateTime = new DateTime(2021, 4, 7), OralMark = 30, TotalMark = 100, Courses = new Collection<Course>() { c1, c2, c3, c4, c5 } };

            var assignments = new List<Assignment>() { a1, a2, a3, a4, a5 };

            foreach (var assignment in assignments)
            {
                context.Assignments.AddOrUpdate(t => new { t.Title, t.Description }, assignment);
            }

            var s1 = new Student() { FirstName = "Maria", LastName = "Anagnostou", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c1, c2 }, Assignments = new Collection<Assignment>(){a1,a2}};
            var s2 = new Student() { FirstName = "Antonis", LastName = "Fournaros", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c2, c3 }, Assignments = new Collection<Assignment>(){ a1, a2,a2 } };
            var s3 = new Student() { FirstName = "George", LastName = "Mpekoy", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c3, c4 }, Assignments = new Collection<Assignment>() { a1, a3,a5 } };
            var s4 = new Student() { FirstName = "Katerina", LastName = "Marinos", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c5, c1 }, Assignments = new Collection<Assignment>() { a3, a4, a5 } };
            var s5 = new Student() { FirstName = "Anastasia", LastName = "Psalida", DateOfBirth = new DateTime(1998, 9, 8), TuitionFees = 250, Courses = new Collection<Course>() { c4, c5 }, Assignments = new Collection<Assignment>() { a4, a5 } };

            var students = new List<Student>() { s1, s2, s3, s4, s5 };



            foreach (var student in students)
            {
                context.Students.AddOrUpdate(s => new { s.FirstName, s.LastName, s.DateOfBirth }, student);
            }
            var t1 = new Trainer() { FirstName = "Marios", LastName = "Kanistras", Subject = "Programmer", Courses = new Collection<Course>() { c1, c2 } };
            var t2 = new Trainer() { FirstName = "Kostas", LastName = "Papadopoulos", Subject = "Programmer", Courses = new Collection<Course>() { c2, c3 } };
            var t3 = new Trainer() { FirstName = "Thanos", LastName = "Kanelopoulos", Subject = "Programmer", Courses = new Collection<Course>() { c3, c4 } };
            var t4 = new Trainer() { FirstName = "Katerina", LastName = "Anastasiou", Subject = "Database", Courses = new Collection<Course>() { c1, c2, c5 } };
            var t5 = new Trainer() { FirstName = "Maria", LastName = "Papadatou", Subject = "Database", Courses = new Collection<Course>() { c4, c3, c5 } };

            var trainers = new List<Trainer>() { t1, t2, t3, t4, t5 };

            foreach (var trainer in trainers)
            {
                context.Trainers.AddOrUpdate(t => new { t.FirstName, t.LastName }, trainer);
            }

            context.SaveChanges();
        }
    }
}
