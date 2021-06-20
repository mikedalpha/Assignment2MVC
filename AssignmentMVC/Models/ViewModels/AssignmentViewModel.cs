using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public readonly AssignmentRepos AssignmentRepos;
        public readonly StudentRepos StudentRepos;
        public readonly CourseRepos CourseRepos;

        public Assignment Assignment { get; set; }

        public AssignmentViewModel(AssignmentRepos assignmentRepos, StudentRepos studentRepos, CourseRepos courseRepos)
        {
            AssignmentRepos = assignmentRepos;
            StudentRepos = studentRepos;
            CourseRepos = courseRepos;
        }

        public AssignmentViewModel(AssignmentRepos assignmentRepos, StudentRepos studentRepos, CourseRepos courseRepos, Assignment assignment)
        {
            AssignmentRepos = assignmentRepos;
            StudentRepos = studentRepos;
            CourseRepos = courseRepos;
            Assignment = assignment;
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
                AssignmentRepos.AttachAssignmentCourses(Assignment);
                var selectedIds = Assignment.Courses.Select(c => c.CourseId);

                return CourseRepos.Get().Select(c => new SelectListItem()
                {
                    Value = c.CourseId.ToString(),
                    Text = string.Format($"{c.Title} {c.Stream}"),
                    Selected = selectedIds.Any(id=>id == c.CourseId)
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
                AssignmentRepos.AttachAssignmentStudents(Assignment);
                var selectedIds = Assignment.Students.Select(s => s.StudentId);

                return StudentRepos.Get().Select(s => new SelectListItem()
                {
                    Value = s.StudentId.ToString(),
                    Text = string.Format($"{s.FirstName} {s.LastName}"),
                    Selected = selectedIds.Any(id=>id==s.StudentId)
                });
            }
        }
    }
}