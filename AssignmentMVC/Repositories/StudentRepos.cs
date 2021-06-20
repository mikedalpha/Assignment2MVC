using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;

namespace AssignmentMVC.Repositories
{
    public class StudentRepos
    {
        private readonly ApplicationDbContext dbContext;

        public StudentRepos()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Student> Get()
        {
            var students = dbContext.Students.ToList();
            return students;
        }

        public Student Find(int? id)
        {
            return dbContext.Students.Find(id);
        }

        public void AttachStudentCourses(Student student)
        {
            dbContext.Students.Attach(student);
            dbContext.Entry(student).Collection("Courses").Load();
        }

        public void ClearStudentCourses(Student student)
        {
            student.Courses.Clear();
        }

        public void AssignStudentCourses(Student student, IEnumerable<int> CourseList)
        {
            if (CourseList != null)
            {
                foreach (var id in CourseList)
                {
                    Course course = dbContext.Courses.Find(id);

                    if (course != null)
                    {
                        student.Courses.Add(course);
                    }
                }
            }

            SaveChanges();
        }

        public void AttachStudentAssignments(Student student)
        {
            dbContext.Students.Attach(student);
            dbContext.Entry(student).Collection("Assignments").Load();
        }

        public void ClearStudentAssignments(Student student)
        {
            student.Assignments.Clear();
        }

        public void AssignStudentAssignments(Student student, IEnumerable<int> AssignmentList)
        {
            if (AssignmentList != null)
            {
                foreach (var id in AssignmentList)
                {
                    Assignment assignment = dbContext.Assignments.Find(id);
                    if (assignment != null)
                    {
                        student.Assignments.Add(assignment);
                    }
                }
            }

            SaveChanges();
        }

        public void Create(Student student)
        {
            dbContext.Entry(student).State = EntityState.Added;
            SaveChanges();
        }

        public void Edit(Student student)
        {
            dbContext.Entry(student).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Student student)
        {
            dbContext.Entry(student).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

    }
}