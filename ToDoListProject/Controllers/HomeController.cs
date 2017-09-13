using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoListProject.Models;
using ToDoListProject.Repositories;

namespace ToDoListProject.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        
        public HomeController()
        {
            this._repository = new Repository(new ToDoListContext());
        }

        public HomeController(Repository repository)
        {
            this._repository = repository;
        }

        public ActionResult Index()
        {
            List<ToDoListItem> ToDoList;
            if (Session["ToDoList"] != null)
            {
                ToDoList = (List<ToDoListItem>)Session["ToDoList"];
            }
            else
            {
                ToDoList = new List<ToDoListItem>();
            }
            return View("Index", ToDoList);
        }

        public ActionResult Get()
        {
            List<ToDoListItem> model = new List<ToDoListItem>();
            ViewBag.ToDoList = _repository.Get();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                List<ToDoListItem> ToDoList = (List<ToDoListItem>)Session["ToDoList"];
                int newId = 1;
                if (ToDoList != null)
                {
                    List<int> ids = new List<int>();
                    foreach (ToDoListItem listItem in ToDoList)
                    {
                        ids.Add(listItem.Id);
                    }
                    if (ids != null)
                    {
                        newId = ids.Max() + 1;
                    }
                }
                ToDoListItem item = new ToDoListItem();
                item.Id = newId;
                item.Task = collection["Task"];
                item.Status = collection["Status"];
                item.StartDate = Convert.ToDateTime(collection["StartDate"]);
                item.EndDate = Convert.ToDateTime(collection["EndDate"]);
                TempData["TodoList"] =  _repository.Add(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit
        public ActionResult Edit(int id)
        {
            ToDoListItem editItem = _repository.GetItem(id);
            return View("Edit", editItem);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                ToDoListItem item = new ToDoListItem();
                item.Id = Convert.ToInt16(collection["id"]);
                item.Task = collection["Task"];
                item.Status = collection["Status"];
                item.StartDate = Convert.ToDateTime(collection["StartDate"]);
                item.EndDate = Convert.ToDateTime(collection["EndDate"]);

                List<ToDoListItem> NewItem = _repository.SaveItem(item);

                return RedirectToAction("Index", NewItem);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Complete(int id)
        {
            TempData["TodoList"] = _repository.Complete(id);

            return RedirectToAction("Index");
        }
    }
}