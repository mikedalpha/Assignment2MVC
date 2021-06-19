using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Models.ViewModels
{
    public class TrainerCreateViewModel
    {
        public readonly TrainerRepos TrainerRepos;
        public readonly CourseRepos CourseRepos;

        public TrainerCreateViewModel(TrainerRepos trainerRepos, CourseRepos courseRepos)
        {
            TrainerRepos = trainerRepos;
            CourseRepos = courseRepos;
        }

        public Trainer Trainer { get; set; }

        public IEnumerable<SelectListItem> SelectedCourseIds
        {
            get
            {
                return CourseRepos.Get().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}")
                });
            }
        }

        
    }
}