using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;
using Microsoft.SqlServer.Server;

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

        public Trainer Find(int? id)
        {
            return dbContext.Trainers.Find(id);
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