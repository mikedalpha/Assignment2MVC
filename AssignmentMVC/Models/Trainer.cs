using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace AssignmentMVC.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Trainer()
        {
            Courses = new HashSet<Course>();
        }
    }
}