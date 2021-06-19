using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AssignmentMVC.Models;
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
            ViewBag.SelectedCourseIds = courseRepos.Get().Select(c => new SelectListItem()
            {
                Value = c.CourseId.ToString(),
                Text = c.Title
            });

            return View();
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Subject")] Trainer trainer, IEnumerable<int> SelectedCourseIds)
        {
            if (ModelState.IsValid)
            {
                trainerRepos.AttachTrainerCourses(trainer);
                trainerRepos.SaveChanges();
                trainerRepos.AssignTrainerCourses(trainer, SelectedCourseIds);
                //trainerRepos.Create(trainer, SelectedCourseIds);
                trainerRepos.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainer);
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
            return View(trainer);
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerId,FirstName,LastName,Subject")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                trainerRepos.Edit(trainer);
                return RedirectToAction("Index");
            }
            return View(trainer);
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
