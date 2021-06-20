using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.DAL;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseRepos courseRepos;
        private readonly TrainerRepos trainerRepos;
        private readonly StudentRepos studentRepos;
        private readonly AssignmentRepos assignmentRepos;

        public CourseController()
        {
            courseRepos = new CourseRepos();
            trainerRepos = new TrainerRepos();
            studentRepos = new StudentRepos();
            assignmentRepos = new AssignmentRepos();
        }
        
        // GET: Course
        public ActionResult Index()
        {
            var courses = courseRepos.Get();
            return View(courses);
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = courseRepos.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            var vm = new CourseViewModel(courseRepos, trainerRepos, studentRepos, assignmentRepos);

            return View(vm);
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course, IEnumerable<int> TrainerList, IEnumerable<int> StudentList, IEnumerable<int> AssignmentList)
        {
            if (ModelState.IsValid)
            {
                courseRepos.AssignCourseTrainers(course, TrainerList);
                courseRepos.AssignCourseStudents(course, StudentList);
                courseRepos.AssignCourseAssignments(course, AssignmentList);
                courseRepos.Create(course);

                return RedirectToAction("Index");
            }

            var vm = new CourseViewModel(courseRepos, trainerRepos, studentRepos, assignmentRepos);

            return View(vm);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = courseRepos.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            var vm = new CourseViewModel(courseRepos, trainerRepos, studentRepos, assignmentRepos, course);

            return View(vm);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course, IEnumerable<int> SelectedTrainerList, IEnumerable<int> SelectedStudentList, IEnumerable<int> SelectedAssignmentList)
        {
            if (ModelState.IsValid)
            {
                courseRepos.AttachCourseTrainers(course);
                courseRepos.ClearCourseTrainers(course);
                courseRepos.SaveChanges();
                courseRepos.AssignCourseTrainers(course, SelectedTrainerList);

                courseRepos.AttachCourseStudents(course);
                courseRepos.ClearCourseStudents(course);
                courseRepos.SaveChanges();
                courseRepos.AssignCourseStudents(course, SelectedStudentList);

                courseRepos.AttachCourseAssignments(course);
                courseRepos.ClearCourseAssignments(course);
                courseRepos.SaveChanges();
                courseRepos.AssignCourseAssignments(course, SelectedAssignmentList);

                courseRepos.Edit(course);

                return RedirectToAction("Index");
            }

            var vm = new CourseViewModel(courseRepos, trainerRepos, studentRepos, assignmentRepos, course);

            return View(vm);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = courseRepos.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = courseRepos.Find(id);
            courseRepos.Delete(course);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                courseRepos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
