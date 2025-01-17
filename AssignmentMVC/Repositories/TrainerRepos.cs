﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;

namespace AssignmentMVC.Repositories
{
    public class TrainerRepos
    {
        private readonly ApplicationDbContext dbContext;
        

        public TrainerRepos()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Trainer> Get()
        {
            var trainers = dbContext.Trainers.ToList();
            return trainers;
        }

        public DbSet<Trainer> TrainerDbSet()
        {
            return dbContext.Trainers;
        }

        public Trainer Find(int? id)
        {
            return dbContext.Trainers.Find(id);
        }

        public void AttachTrainerCourses(Trainer trainer)
        {
            dbContext.Trainers.Attach(trainer);
            dbContext.Entry(trainer).Collection("Courses").Load();
        }

        public void ClearTrainerCourses(Trainer trainer)
        {
            trainer.Courses.Clear();
        }

        public void AssignTrainerCourses(Trainer trainer, IEnumerable<int> CourseList)
        {
            if (CourseList != null)
            {
                foreach (var id in CourseList)
                {
                    Course course = dbContext.Courses.Find(id);
                    if (course != null)
                    {
                        trainer.Courses.Add(course);
                    }
                }
            }

            SaveChanges();
        }

        public void Create(Trainer trainer)
        {
            dbContext.Entry(trainer).State = EntityState.Added;
            SaveChanges();
        }

        public void Edit(Trainer trainer)
        {
            dbContext.Entry(trainer).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Trainer trainer)
        {
            dbContext.Entry(trainer).State = EntityState.Deleted;
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