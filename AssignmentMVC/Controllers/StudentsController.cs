using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentRepos studentRepos;
        private readonly AssignmentRepos assignmentRepos;
        private readonly CourseRepos courseRepos;

        public StudentsController()
        {
            studentRepos = new StudentRepos();
            assignmentRepos = new AssignmentRepos();
            courseRepos = new CourseRepos();
        }

        // GET: Students
        public ActionResult Index()
        {
            var students = studentRepos.Get();
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = studentRepos.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var vm = new StudentViewModel(studentRepos, courseRepos, assignmentRepos);

            return View(vm);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,TuitionFees")] Student student, IEnumerable<int> CourseList, IEnumerable<int> AssignmentList)
        {
            if (ModelState.IsValid)
            {
                studentRepos.AssignStudentCourses(student, CourseList);
                studentRepos.AssignStudentAssignments(student, AssignmentList);
                studentRepos.Create(student);
                return RedirectToAction("Index");
            }

            var vm = new StudentViewModel(studentRepos, courseRepos, assignmentRepos);

            return View(vm);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = studentRepos.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            var vm = new StudentViewModel(studentRepos, courseRepos, assignmentRepos, student);

            return View(vm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,TuitionFees")] Student student, IEnumerable<int> SelectedCourseList, IEnumerable<int> SelectedAssignmentList)
        {
            if (ModelState.IsValid)
            {
                studentRepos.AttachStudentCourses(student);
                studentRepos.ClearStudentCourses(student);
                studentRepos.SaveChanges();
                studentRepos.AssignStudentCourses(student, SelectedCourseList);

                studentRepos.AttachStudentAssignments(student);
                studentRepos.ClearStudentAssignments(student);
                studentRepos.SaveChanges();
                studentRepos.AssignStudentAssignments(student, SelectedAssignmentList);

                studentRepos.Edit(student);
                return RedirectToAction("Index");
            }

            var vm = new StudentViewModel(studentRepos, courseRepos, assignmentRepos, student);

            return View(vm);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepos.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = studentRepos.Find(id);
            studentRepos.Delete(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studentRepos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
