using System.Data.Entity.ModelConfiguration;
using AssignmentMVC.Models;

namespace AssignmentMVC.DAL.Configurations
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            //Students Table

            Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.DateOfBirth)
                .IsRequired()
                .HasColumnType("Date");

            Property(s => s.TuitionFees)
                .IsRequired();


            //StudentsAssignments
            HasMany(s => s.Assignments)
                .WithMany(a => a.Students)
                .Map(m =>
                {
                    m.ToTable("StudentsAssignments");
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("AssignmentId");
                });
        }
    }
}