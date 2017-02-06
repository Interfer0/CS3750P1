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
    public class TodoListsController : Controller
    {
        private Project1TodoEntities db = new Project1TodoEntities();

        // GET: TodoLists
        public ActionResult Index()
        {
            return View(db.TodoLists.ToList());
        }
        /*
        // GET: TodoLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }
        */
        // GET: TodoLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "todolistid,title")] TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                if (todoList.title == null)
                    todoList.title = "not named";
                db.TodoLists.Add(todoList);
                db.SaveChanges();
                return RedirectToAction("Edit/"+todoList.todolistid.ToString());
            }

            return View(todoList);
        }
        
        // GET: TodoLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }

        // POST: TodoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "todolistid,title")] TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoList);
        }
        /*
        // GET: TodoLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }

        // POST: TodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoList todoList = db.TodoLists.Find(id);
            db.TodoLists.Remove(todoList);
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



        public int UpdateTitle(String title,String listID)
        {
            TodoList toList = db.TodoLists.Find(int.Parse(listID));
            toList.title = title;
            db.SaveChanges();
            return toList.todolistid;
        }

        public ActionResult DeleteList(String listid)
        {
            int ID = int.Parse(listid);
            TodoList toList = db.TodoLists.Find(int.Parse(listid));
            var  catList = db.CategoryTodoLists.Where(L => L.todolistid == ID);
            foreach(CategoryTodoList c in catList)
            {
                db.CategoryTodoLists.Remove(c);
            }
            var itmList = db.TodoItems.Where(L => L.todolistid == ID);
            foreach(TodoItem i in itmList)
            {
                db.TodoItems.Remove(i);
            }
            db.TodoLists.Remove(toList);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
