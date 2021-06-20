using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;

namespace AssignmentMVC.Repositories
{
    public class CourseRepos
    {
        private readonly ApplicationDbContext dbContext;

        public CourseRepos()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Course> Get()
        {
            var courses = dbContext.Courses.ToList();
            return courses;
        }

        public Course Find(int? id)
        {
            return dbContext.Courses.Find(id);
        }

        public void AttachCourseTrainers(Course course)
        {
            dbContext.Courses.Attach(course);
            dbContext.Entry(course).Collection("Trainers").Load();
        }

        public void ClearCourseTrainers(Course course)
        {
            course.Trainers.Clear();
        }

        public void AssignCourseTrainers(Course course, IEnumerable<int> TrainerList)
        {
            if (TrainerList != null)
            {
                foreach (var id in TrainerList)
                {
                    Trainer trainer = dbContext.Trainers.Find(id);

                    if (trainer != null)
                    {
                        course.Trainers.Add(trainer);
                    }
                }
            }

            SaveChanges();
        }

        public void AttachCourseStudents(Course course)
        {
            dbContext.Courses.Attach(course);
            dbContext.Entry(course).Collection("Students").Load();
        }

        public void ClearCourseStudents(Course course)
        {
            course.Students.Clear();
        }

        public void AssignCourseStudents(Course course, IEnumerable<int> StudentList)
        {
            if (StudentList != null)
            {
                foreach (var id in StudentList)
                {
                    Student student = dbContext.Students.Find(id);

                    if (student != null)
                    {
                        course.Students.Add(student);
                    }
                }
            }

            SaveChanges();
        }
        public void AttachCourseAssignments(Course course)
        {
            dbContext.Courses.Attach(course);
            dbContext.Entry(course).Collection("Assignments").Load();
        }

        public void ClearCourseAssignments(Course course)
        {
            course.Assignments.Clear();
        }

        public void AssignCourseAssignments(Course course, IEnumerable<int> AssignmentList)
        {
            if (AssignmentList != null)
            {
                foreach (var id in AssignmentList)
                {
                    Assignment assignment = dbContext.Assignments.Find(id);

                    if (assignment != null)
                    {
                        course.Assignments.Add(assignment);
                    }
                }
            }

            SaveChanges();
        }

        public void Create(Course course)
        {
            dbContext.Entry(course).State = EntityState.Added;
            SaveChanges();
        }

        public void Edit(Course course)
        {
            dbContext.Entry(course).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Course course)
        {
            dbContext.Entry(course).State = EntityState.Deleted;
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