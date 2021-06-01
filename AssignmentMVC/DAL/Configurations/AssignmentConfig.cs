using System.Data.Entity.ModelConfiguration;
using AssignmentMVC.Models;

namespace AssignmentMVC.DAL.Configurations
{
    public class AssignmentConfig : EntityTypeConfiguration<Assignment>
    {
        public AssignmentConfig()
        {
            Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.SubDateTime)
                .IsRequired()
                .HasColumnType("Date");

            Property(a => a.OralMark)
                .IsRequired();

            Property(a => a.TotalMark)
                .IsRequired();
        }
    }
}