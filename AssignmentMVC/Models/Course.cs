using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentMVC.Models.Validations;
using FluentValidation.Attributes;

namespace AssignmentMVC.Models
{
    [Validator(typeof(CourseValidator))]
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        public Course()
        {
            Trainers = new HashSet<Trainer>();
            Students = new HashSet<Student>();
            Assignments = new HashSet<Assignment>();
        }

    }
}