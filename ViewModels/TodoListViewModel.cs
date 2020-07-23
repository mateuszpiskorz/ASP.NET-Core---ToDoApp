using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ToDoListWebApp.Helpers;
using ToDoListWebApp.Models;

namespace ToDoListWebApp.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new TodoListItem();
                this.TodoItems = db.Query<TodoListItem>("SELECT * FROM TodoListItems ORDER BY AddDate DESC").ToList();            
            }
        }

        public List<TodoListItem> TodoItems { get; set; }
        public TodoListItem EditableItem { get; set; }
    }
}
