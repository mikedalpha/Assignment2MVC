using System.Data.Entity;
using AssignmentMVC.DAL.Configurations;
using AssignmentMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssignmentMVC.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TrainerConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new AssignmentConfig());
            modelBuilder.Configurations.Add(new CourseConfig());
        }
    }
}