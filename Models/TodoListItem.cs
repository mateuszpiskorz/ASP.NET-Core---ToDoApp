using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoListWebApp.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage ="Title must contain at least 2 characters.")]
        [MaxLength(200, ErrorMessage ="Title can't exceed 200 characters.")]
        public string Title { get; set; }
        public bool isDone { get; set; }
    }
}
