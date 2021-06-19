using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssignmentMVC.Models;
using AssignmentMVC.Repositories;

namespace AssignmentMVC.Controllers
{
    public class TrainerController : Controller
    {
        private readonly TrainerRepos _repos;

        public TrainerController()
        {
            _repos = new TrainerRepos();
        }

        // GET: Trainer
        public ActionResult Index()
        {
            var trainers = _repos.Get();
            return View(trainers);
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = _repos.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            //ViewBag.SelectedTrainerIds = _repos.Get().Select(t => new SelectListItem()
            //{
            //    Value = t.TrainerId.ToString(),
            //    Text = $"{t.FirstName} {t.LastName}"
            //});



            return View();
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Subject")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _repos.Create(trainer);
                _repos.SaveChanges();
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
            Trainer trainer = _repos.Find(id);
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
                _repos.Edit(trainer);
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
            Trainer trainer = _repos.Find(id);
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
            Trainer trainer = _repos.Find(id);
            _repos.Delete(trainer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
