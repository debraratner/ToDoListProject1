using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace ToDoListProject.Models
{
    public class ToDoListContext : Context
    {
        public ToDoListContext() : base()
        {
        }
        public List<ToDoListItem> ToDoList { get; set; }
    }
} 
