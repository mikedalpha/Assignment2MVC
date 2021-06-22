using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;
using PagedList;

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.currentName = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.StreamSortParm = sortOrder == "Stream" ? "stream_desc" : "Stream";
            ViewBag.StartDateSortParm = sortOrder == "Start Date" ? "startDate_desc" : "Start Date";
            ViewBag.EndDateSortParm = sortOrder == "End Date" ? "endDate_desc" : "End Date";

            var courses = courseRepos.Get();

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
                courses = courses.Where(c => c.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(c => c.Title);
                    break;
                case "Stream":
                    courses = courses.OrderBy(c => c.Stream);
                    break;
                case "stream_desc":
                    courses = courses.OrderByDescending(c => c.Stream);
                    break;
                case "Start Date":
                    courses = courses.OrderBy(c => c.StartDate);
                    break;
                case "startDate_desc":
                    courses = courses.OrderByDescending(c => c.StartDate);
                    break;
                case "End Date":
                    courses = courses.OrderBy(c => c.EndDate);
                    break;
                case "endDate_desc":
                    courses = courses.OrderByDescending(c => c.EndDate);
                    break;
                default:
                    courses = courses.OrderBy(c => c.Title);
                    break;
            }
            #endregion

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(courses.ToPagedList(pageNumber, pageSize));
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
