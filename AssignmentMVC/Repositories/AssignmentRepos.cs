using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;

namespace AssignmentMVC.Repositories
{
    public class AssignmentRepos
    {
        private readonly ApplicationDbContext dbContext;

        public AssignmentRepos()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Assignment> Get()
        {
            var assignments = dbContext.Assignments.ToList();
            return assignments;
        }
        public Assignment Find(int? id)
        {
            return dbContext.Assignments.Find(id);
        }

        public void AttachAssignmentCourses(Assignment assignment)
        {
            dbContext.Assignments.Attach(assignment);
            dbContext.Entry(assignment).Collection("Courses").Load();
        }

        public void ClearAssignmentCourses(Assignment assignment)
        {
            assignment.Courses.Clear();
        }

        public void AssignAssignmentCourses(Assignment assignment, IEnumerable<int> CourseList)
        {
            if (CourseList != null)
            {
                foreach (var id in CourseList)
                {
                    Course course = dbContext.Courses.Find(id);

                    if (course != null)
                    {
                        assignment.Courses.Add(course);
                    }
                }
            }

            SaveChanges();
        }

        public void AttachAssignmentStudents(Assignment assignment)
        {
            dbContext.Assignments.Attach(assignment);
            dbContext.Entry(assignment).Collection("Students").Load();
        }

        public void ClearAssignmentStudents(Assignment assignment)
        {
            assignment.Students.Clear();
        }

        public void AssignAssignmentStudents(Assignment assignment, IEnumerable<int> StudentList)
        {
            if (StudentList != null)
            {
                foreach (var id in StudentList)
                {
                    Student student = dbContext.Students.Find(id);

                    if (student != null)
                    {
                        assignment.Students.Add(student);
                    }
                }
            }

            SaveChanges();
        }

        public void Create(Assignment assignment)
        {
            dbContext.Entry(assignment).State = EntityState.Added;
            SaveChanges();
        }

        public void Edit(Assignment assignment)
        {
            dbContext.Entry(assignment).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Assignment assignment)
        {
            dbContext.Entry(assignment).State = EntityState.Deleted;
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