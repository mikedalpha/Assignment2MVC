using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;

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
        public ActionResult Index()
        {
            var assignments = assignmentRepos.Get();
            return View(assignments);
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
