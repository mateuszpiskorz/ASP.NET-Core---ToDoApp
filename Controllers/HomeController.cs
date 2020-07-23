using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebApp.ViewModels;
using ToDoListWebApp.Helpers;
using ToDoListWebApp.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace ToDoListWebApp.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null)
                {
                    db.Delete(item);
                }
                return RedirectToAction("Index");

            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<TodoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        TodoListItem dbItem = db.Get<TodoListItem>(viewModel.EditableItem.Id);
                        TryUpdateModelAsync<TodoListItem>(dbItem, "EditableItem");
                        db.Update<TodoListItem>(dbItem);
                    }
                    return RedirectToAction("Index");

                }
            }
            else
                return View("Index", new TodoListViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null)
                {
                    item.isDone = !item.isDone;
                    db.Update<TodoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}
