using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;
using PagedList;

namespace AssignmentMVC.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly StudentRepos studentRepos;
        private readonly AssignmentRepos assignmentRepos;
        private readonly CourseRepos courseRepos;

        public AssignmentController()
        {
            studentRepos = new StudentRepos();
            assignmentRepos = new AssignmentRepos();
            courseRepos = new CourseRepos();
        }

        // GET: Assignment
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.currentName = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.SubmissionDateSortParm = sortOrder == "Submission Date" ? "submissionDate_desc" : "Submission Date";
            ViewBag.OralMarkSortParm = sortOrder == "Oral Mark" ? "oralMark_desc" : "Oral Mark";
            ViewBag.TotalMarkSortParm = sortOrder == "Total Mark" ? "totalMark_desc" : "Total Mark";

            var assignments = assignmentRepos.Get();

            #region Pagination
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            #endregion

            #region Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                assignments = assignments.Where(a => a.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "title_desc":
                    assignments = assignments.OrderByDescending(a => a.Title);
                    break;
                case "Submission Date":
                    assignments = assignments.OrderBy(a => a.SubDateTime);
                    break;
                case "submissionDate_desc":
                    assignments = assignments.OrderByDescending(a => a.SubDateTime);
                    break;
                case "Oral Mark":
                    assignments = assignments.OrderBy(a => a.OralMark);
                    break;
                case "oralMark_desc":
                    assignments = assignments.OrderByDescending(a => a.OralMark);
                    break;
                case "Total Mark":
                    assignments = assignments.OrderBy(a => a.TotalMark);
                    break;
                case "totalMark_desc":
                    assignments = assignments.OrderByDescending(a => a.TotalMark);
                    break;
                default:
                    assignments = assignments.OrderBy(a => a.Title);
                    break;
            }
            #endregion

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(assignments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = assignmentRepos.Find(id);

            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            var vm = new AssignmentViewModel(assignmentRepos, studentRepos, courseRepos);

            return View(vm);
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId,Title,Description,SubDateTime,OralMark,TotalMark")] Assignment assignment, IEnumerable<int> StudentList, IEnumerable<int> CourseList)
        {
            if (ModelState.IsValid)
            {
                assignmentRepos.AssignAssignmentStudents(assignment, StudentList);
                assignmentRepos.AssignAssignmentCourses(assignment, CourseList);
                assignmentRepos.Create(assignment);
                return RedirectToAction("Index");
            }

            var vm = new AssignmentViewModel(assignmentRepos, studentRepos, courseRepos);

            return View(vm);
        }

        // GET: Assignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = assignmentRepos.Find(id);

            if (assignment == null)
            {
                return HttpNotFound();
            }

            var vm = new AssignmentViewModel(assignmentRepos, studentRepos, courseRepos, assignment);

            return View(vm);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentId,Title,Description,SubDateTime,OralMark,TotalMark")] Assignment assignment, IEnumerable<int> SelectedStudentList, IEnumerable<int> SelectedCourseList)
        {
            if (ModelState.IsValid)
            {
                assignmentRepos.AttachAssignmentStudents(assignment);
                assignmentRepos.ClearAssignmentStudents(assignment);
                assignmentRepos.SaveChanges();
                assignmentRepos.AssignAssignmentStudents(assignment, SelectedStudentList);

                assignmentRepos.AttachAssignmentCourses(assignment);
                assignmentRepos.ClearAssignmentCourses(assignment);
                assignmentRepos.SaveChanges();
                assignmentRepos.AssignAssignmentCourses(assignment, SelectedCourseList);

                assignmentRepos.Edit(assignment);

                return RedirectToAction("Index");
            }

            var vm = new AssignmentViewModel(assignmentRepos, studentRepos, courseRepos, assignment);

            return View(vm);
        }

        // GET: Assignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = assignmentRepos.Find(id);

            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = assignmentRepos.Find(id);
            assignmentRepos.Delete(assignment);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                assignmentRepos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
