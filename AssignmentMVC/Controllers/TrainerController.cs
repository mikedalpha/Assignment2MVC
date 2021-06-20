using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Models.ViewModels;
using AssignmentMVC.Repositories;

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
        public ActionResult Index()
        {
            var trainers = trainerRepos.Get();
            return View(trainers);
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
