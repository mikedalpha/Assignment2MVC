using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Models.ViewModels
{
    public class StudentViewModel
    {
        public readonly StudentRepos StudentRepos;
        public readonly CourseRepos CourseRepos;
        public readonly AssignmentRepos AssignmentRepos;

        public Student Student { get; set; }

        public StudentViewModel(StudentRepos studentRepos, CourseRepos courseRepos, AssignmentRepos assignmentRepos)
        {
            StudentRepos = studentRepos;
            CourseRepos = courseRepos;
            AssignmentRepos = assignmentRepos;
        }

        public StudentViewModel(StudentRepos studentRepos, CourseRepos courseRepos, AssignmentRepos assignmentRepos, Student student)
        {
            StudentRepos = studentRepos;
            CourseRepos = courseRepos;
            AssignmentRepos = assignmentRepos;
            Student = student;
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
                StudentRepos.AttachStudentCourses(Student);
                var selectedIds = Student.Courses.Select(c => c.CourseId);
                return CourseRepos.Get().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = selectedIds.Any(id => id == c.CourseId)
                }); ;
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
                StudentRepos.AttachStudentAssignments(Student);
                var selectedIds = Student.Assignments.Select(a => a.AssignmentId);

                return AssignmentRepos.Get().Select(a => new SelectListItem()
                {
                    Value = a.AssignmentId.ToString(),
                    Text = a.Title.ToString(),
                    Selected = selectedIds.Any(id => id == a.AssignmentId)
                }); ;
            }
        }

    }
}