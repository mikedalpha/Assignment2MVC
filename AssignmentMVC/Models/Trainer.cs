using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentMVC.Models.Validations;
using FluentValidation.Attributes;

namespace AssignmentMVC.Models
{
    [Validator(typeof(TrainerValidator))]
    public class Trainer
    {
        public int TrainerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Subject { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Trainer()
        {
            Courses = new HashSet<Course>();
        }
    }
}