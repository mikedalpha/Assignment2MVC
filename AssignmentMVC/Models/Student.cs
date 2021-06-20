using System;
using System.Collections.Generic;
using System.ComponentModel;
using AssignmentMVC.Models.Validations;
using FluentValidation.Attributes;

namespace AssignmentMVC.Models
{
    [Validator(typeof(StudentValidator))]
    public class Student
    {
        public int StudentId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Tuition Fees")]
        public int TuitionFees { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        public Student()
        {
            Courses = new HashSet<Course>();
            Assignments = new HashSet<Assignment>();
        }

    }
}