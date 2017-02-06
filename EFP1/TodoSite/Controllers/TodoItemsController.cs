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
    public class TodoItemsController : Controller
    {
        private Project1TodoEntities db = new Project1TodoEntities();

        // GET: TodoItems
        public ActionResult Index()
        {
            var todoItems = db.TodoItems.Include(t => t.TodoList);
            return View(todoItems.ToList());
        }
        /*
        // GET: TodoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // GET: TodoItems/Create
        public ActionResult Create()
        {
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title");
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "todoitemid,todolistid,title,description,complete")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                db.TodoItems.Add(todoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", todoItem.todolistid);
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", todoItem.todolistid);
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "todoitemid,todolistid,title,description,complete")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.todolistid = new SelectList(db.TodoLists, "todolistid", "title", todoItem.todolistid);
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            db.TodoItems.Remove(todoItem);
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
        */
        public int Insert(String title, String description, String listid)
        {
            TodoItem toItem = new TodoItem();
            toItem.todolistid = int.Parse(listid);
            toItem.title = title;
            toItem.description = description;            
            toItem.complete = 0;
            db.TodoItems.Add(toItem);
            db.SaveChanges();
            return toItem.todoitemid;
        }

        
        public int DeleteItem(String itemid)
        {

            TodoItem toItem = db.TodoItems.Find(int.Parse(itemid));
            db.TodoItems.Remove(toItem);
            db.SaveChanges();
            return toItem.todoitemid;
        }
    }
}
