using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Models
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; } = false;
        public  List<SubTaskDto> SubTasks { get; set; }
    }
}
