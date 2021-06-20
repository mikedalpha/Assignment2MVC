using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Models.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }

        public readonly CourseRepos CourseRepos;
        public readonly TrainerRepos TrainerRepos;
        public readonly StudentRepos StudentRepos;
        public readonly AssignmentRepos AssignmentRepos;

        public CourseViewModel(CourseRepos courseRepos, TrainerRepos trainerRepos, StudentRepos studentRepos, AssignmentRepos assignmentRepos)
        {
            CourseRepos = courseRepos;
            TrainerRepos = trainerRepos;
            StudentRepos = studentRepos;
            AssignmentRepos = assignmentRepos;
        }

        public CourseViewModel(CourseRepos courseRepos, TrainerRepos trainerRepos, StudentRepos studentRepos, AssignmentRepos assignmentRepos, Course course)
        {
            CourseRepos = courseRepos;
            TrainerRepos = trainerRepos;
            StudentRepos = studentRepos;
            AssignmentRepos = assignmentRepos;
            Course = course;
        }
        public IEnumerable<SelectListItem> TrainerList
        {
            get
            {
                return TrainerRepos.Get().Select(t => new SelectListItem()
                {
                    Value = t.TrainerId.ToString(),
                    Text = string.Format($"{t.FirstName} {t.LastName}")
                });
            }
        }

        public IEnumerable<SelectListItem> SelectedTrainerList
        {
            get
            {
                CourseRepos.AttachCourseTrainers(Course);
                var selectedIds = Course.Trainers.Select(t => t.TrainerId);

                return TrainerRepos.Get().Select(t => new SelectListItem()
                {
                    Value = t.TrainerId.ToString(),
                    Text = string.Format($"{t.FirstName} {t.LastName}"),
                    Selected = selectedIds.Any(id=>id==t.TrainerId)
                });
            }
        }

        public IEnumerable<SelectListItem> StudentList
        {
            get
            {
                return StudentRepos.Get().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}")
                });
            }
        }

        public IEnumerable<SelectListItem> SelectedStudentList
        {
            get
            {
                CourseRepos.AttachCourseStudents(Course);
                var selectedIds = Course.Students.Select(s => s.StudentId);

                return StudentRepos.Get().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}"),
                    Selected = selectedIds.Any(id=>id==s.StudentId)
                });
            }
        }
        public IEnumerable<SelectListItem> AssignmentList
        {
            get
            {
                return AssignmentRepos.Get().Select(a => new SelectListItem()
                {
                    Value = a.AssignmentId.ToString(),
                    Text = a.Title.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> SelectedAssignmentList
        {
            get
            {
                CourseRepos.AttachCourseAssignments(Course);
                var selectedIds = Course.Assignments.Select(a => a.AssignmentId);

                return AssignmentRepos.Get().Select(a => new SelectListItem()
                {
                    Value = a.AssignmentId.ToString(),
                    Text = a.Title.ToString(),
                    Selected = selectedIds.Any(id => id == a.AssignmentId)
                });
            }
        }
    }
}