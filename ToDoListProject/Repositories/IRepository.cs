using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListProject.Models;

namespace ToDoListProject.Repositories
{
    interface IRepository
    {
        List<ToDoListItem> Get();
        ToDoListItem GetItem(int id);
        List<ToDoListItem> Complete(int id);
        List<ToDoListItem> Add(ToDoListItem entity);
        List<ToDoListItem> SaveItem(ToDoListItem entity);
    }
}
