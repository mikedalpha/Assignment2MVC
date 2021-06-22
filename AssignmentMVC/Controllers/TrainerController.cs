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
    public class TrainerController : Controller
    {
        private readonly TrainerRepos trainerRepos;
        private readonly CourseRepos courseRepos;

        public TrainerController()
        {
            trainerRepos = new TrainerRepos();
            courseRepos = new CourseRepos();
        }

        // GET: Trainer
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.currentName = searchString;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SubjectSortParm = sortOrder == "Subject" ? "subject_desc" : "Subject";

            var trainers = trainerRepos.Get();

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
                trainers = trainers.Where(t => t.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                                               t.LastName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    trainers = trainers.OrderByDescending(t => t.FirstName);
                    break;
                case "Subject":
                    trainers = trainers.OrderBy(t => t.Subject);
                    break;
                case "subject_desc":
                    trainers = trainers.OrderByDescending(t => t.Subject);
                    break;
                default:
                    trainers = trainers.OrderBy(t => t.FirstName);
                    break;
            }
            #endregion

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(trainers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainer trainer = trainerRepos.Find(id);

            if (trainer == null)
            {
                return HttpNotFound();
            }

            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            var vm = new TrainerViewModel(trainerRepos, courseRepos);

            return View(vm);
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Subject")] Trainer trainer, IEnumerable<int> CourseList)
        {
            if (ModelState.IsValid)
            {
                trainerRepos.AssignTrainerCourses(trainer, CourseList);
                trainerRepos.Create(trainer);
                return RedirectToAction("Index");
            }

            var vm = new TrainerViewModel(trainerRepos, courseRepos);

            return View(vm);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainer trainer = trainerRepos.Find(id);

            if (trainer == null)
            {
                return HttpNotFound();
            }

            var vm = new TrainerViewModel(trainerRepos, courseRepos, trainer);

            return View(vm);
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerId,FirstName,LastName,Subject")] Trainer trainer, IEnumerable<int> SelectedCourseList)
        {
            if (ModelState.IsValid)
            {
                trainerRepos.AttachTrainerCourses(trainer);
                trainerRepos.ClearTrainerCourses(trainer);
                trainerRepos.SaveChanges();
                trainerRepos.AssignTrainerCourses(trainer, SelectedCourseList);
                trainerRepos.Edit(trainer);
                return RedirectToAction("Index");
            }

            var vm = new TrainerViewModel(trainerRepos, courseRepos, trainer);

            return View(vm);
        }

        // GET: Trainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainer trainer = trainerRepos.Find(id);

            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = trainerRepos.Find(id);
            trainerRepos.Delete(trainer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                trainerRepos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
