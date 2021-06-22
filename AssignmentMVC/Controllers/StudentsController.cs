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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.currentName = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "Last Name" ? "lastName_desc" : "Last Name";
            ViewBag.DateOfBirthSortParm = sortOrder == "Date of Birth" ? "dateOfBirth_desc" : "Date of Birth";
            ViewBag.TuitionFeesSortParm = sortOrder == "Tuition Fees" ? "tuitionFees_desc" : "Tuition Fees";

            var students = studentRepos.Get();

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
                students = students.Where(s => s.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                                               s.LastName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "Last Name":
                    students = students.OrderBy(s => s.LastName);
                    break;
                case "lastName_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date of Birth":
                    students = students.OrderBy(s => s.DateOfBirth);
                    break;
                case "dateOfBirth_desc":
                    students = students.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "Tuition Fees":
                    students = students.OrderBy(s => s.TuitionFees);
                    break;
                case "tuitionFees_desc":
                    students = students.OrderByDescending(s => s.TuitionFees);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
            #endregion

            int pageSize = 5;
            int pageNumber = (page ?? 1);


            return View(students.ToPagedList(pageNumber, pageSize));
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
