using System;
using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
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