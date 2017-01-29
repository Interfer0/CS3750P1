using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF;

namespace TodoSite.Controllers
{
    public class CategoryTodoListsController : Controller
    {
        private Project1TodoEntities db = new Project1TodoEntities();

        // GET: CategoryTodoLists
        public ActionResult Index()
        {
            var categoryTodoLists = db.CategoryTodoLists.Include(c => c.Category).Include(c => c.TodoList);
            return View(categoryTodoLists.ToList());
        }

        // GET: CategoryTodoLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryTodoList categoryTodoList = db.CategoryTodoLists.Find(id);
            if (categoryTodoList == null)
            {
                return HttpNotFound();
            }
            return View(categoryTodoList);
        }

        // GET: CategoryTodoLists/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.Categories, "categoryid", "title");
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title");
            return View();
        }

        // POST: CategoryTodoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categorytodolistid,todolistid,categoryid")] CategoryTodoList categoryTodoList)
        {
            if (ModelState.IsValid)
            {
                db.CategoryTodoLists.Add(categoryTodoList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.Categories, "categoryid", "title", categoryTodoList.categoryid);
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", categoryTodoList.todolistid);
            return View(categoryTodoList);
        }

        // GET: CategoryTodoLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryTodoList categoryTodoList = db.CategoryTodoLists.Find(id);
            if (categoryTodoList == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.Categories, "categoryid", "title", categoryTodoList.categoryid);
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", categoryTodoList.todolistid);
            return View(categoryTodoList);
        }

        // POST: CategoryTodoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categorytodolistid,todolistid,categoryid")] CategoryTodoList categoryTodoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryTodoList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.Categories, "categoryid", "title", categoryTodoList.categoryid);
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", categoryTodoList.todolistid);
            return View(categoryTodoList);
        }

        // GET: CategoryTodoLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryTodoList categoryTodoList = db.CategoryTodoLists.Find(id);
            if (categoryTodoList == null)
            {
                return HttpNotFound();
            }
            return View(categoryTodoList);
        }

        // POST: CategoryTodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryTodoList categoryTodoList = db.CategoryTodoLists.Find(id);
            db.CategoryTodoLists.Remove(categoryTodoList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
