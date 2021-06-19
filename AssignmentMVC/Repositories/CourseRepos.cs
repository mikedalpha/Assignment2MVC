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