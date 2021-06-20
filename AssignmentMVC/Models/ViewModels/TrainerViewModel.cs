using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Models.ViewModels
{
    public class TrainerViewModel
    {
        public readonly TrainerRepos TrainerRepos;
        public readonly CourseRepos CourseRepos;

        public Trainer Trainer { get; set; }

        public TrainerViewModel(TrainerRepos trainerRepos, CourseRepos courseRepos)
        {
            TrainerRepos = trainerRepos;
            CourseRepos = courseRepos;
        }

        public TrainerViewModel(TrainerRepos trainerRepos, CourseRepos courseRepos, Trainer trainer)
        {
            TrainerRepos = trainerRepos;
            CourseRepos = courseRepos;
            Trainer = trainer;
        }

        public IEnumerable<SelectListItem> CourseList
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
        
        public IEnumerable<SelectListItem> SelectedCourseList
        {
            get
            {
                TrainerRepos.AttachTrainerCourses(Trainer);
                var selectedIds = Trainer.Courses.Select(c => c.CourseId);
                return CourseRepos.Get().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = selectedIds.Any(id=>id==c.CourseId)
                });
            }
        }
    }
}