using System.Data.Entity.ModelConfiguration;
using AssignmentMVC.Models;

namespace AssignmentMVC.DAL.Configurations
{
    public class CourseConfig :EntityTypeConfiguration<Course>
    {

       
        public CourseConfig()
        {
            //Courses Table
            Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Stream)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("Date");

            Property(c => c.EndDate)
                .IsRequired()
                .HasColumnType("Date");

            //TrainersCourses
            HasMany(c => c.Trainers)
                .WithMany(t => t.Courses)
                .Map(m =>
                {
                    m.ToTable("TrainersCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("TrainerId");
                });

            //StudentsCourses
            HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .Map(m =>
                {
                    m.ToTable("StudentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("StudentId");
                });

            //AssignmentsCourses
            HasMany(c => c.Assignments)
                .WithMany(a => a.Courses)
                .Map(m =>
                {
                    m.ToTable("AssignmentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("AssignmentId");
                });
        }
    }
}