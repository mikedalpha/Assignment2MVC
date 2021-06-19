using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Kind { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }

        public Category()
        {
            Trainers = new HashSet<Trainer>();
        }
    }
}