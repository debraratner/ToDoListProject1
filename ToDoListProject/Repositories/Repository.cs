using System;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoListProject.Models;

namespace ToDoListProject.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        List<ToDoListItem> ToDoListContext;

        private ToDoListContext _context;

        public Repository(ToDoListContext context)
        {
            this._context = context;
        }

        public List<ToDoListItem> Get()
        {
            List<ToDoListItem> ToDoList = new List<ToDoListItem>();
            if (ToDoListContext != null)
                ToDoList = ToDoListContext;
            return ToDoList;
        }

        public ToDoListItem GetItem(int itemId)
        {
            List<ToDoListItem> ToDoList = (List<ToDoListItem>)HttpContext.Current.Session["ToDoList"] ;
            ToDoListItem editItem = new ToDoListItem();
            if (ToDoList != null)
            {
                foreach (ToDoListItem item in ToDoList)
                {
                    if (item.Id == itemId)
                    {
                        editItem.Task = item.Task;
                        editItem.Status = item.Status;
                        editItem.EndDate = item.EndDate;
                        editItem.StartDate = item.StartDate;
                        editItem.Id = item.Id;
                    }
                }
            }

            return editItem;
        }

        public List<ToDoListItem> SaveItem(ToDoListItem entity)
        {
            List<ToDoListItem> ToDoListFromSession = (List<ToDoListItem>)HttpContext.Current.Session["ToDoList"];
            if (ToDoListFromSession != null)
            {
                foreach (ToDoListItem item in ToDoListFromSession)
                {
                    if (item.Id == entity.Id)
                    {
                        item.Status = entity.Status;
                        item.EndDate = entity.EndDate;
                        item.StartDate = entity.StartDate;
                        item.Task = entity.Task;
                        
                        break;
                    }
                }
            }
            
            HttpContext.Current.Session["ToDoList"] = ToDoListFromSession;
            return ToDoListFromSession;
        }

        public List<ToDoListItem> Complete(int id)
        {
            List<ToDoListItem> ToDoList = new List<ToDoListItem>();
            ToDoList = (List<ToDoListItem>)HttpContext.Current.Session["ToDoList"];
            foreach (ToDoListItem item in ToDoList)
            {
                if (item.Id == id)
                {
                    item.Status = "Complete";
                    break;
                }
            }
            HttpContext.Current.Session["ToDoList"] = ToDoList;
            return ToDoList;
        }

        public List<ToDoListItem> Add(ToDoListItem entity)
        {
            ToDoListContext = (List<ToDoListItem>)HttpContext.Current.Session["ToDoList"];
            List<ToDoListItem> ToDoList = new List<ToDoListItem>();
            if (ToDoListContext != null)
            {
                ToDoList = ToDoListContext;
            }
            ToDoList.Add(entity);
            HttpContext.Current.Session["ToDoList"] = ToDoList;
            return ToDoList;
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
