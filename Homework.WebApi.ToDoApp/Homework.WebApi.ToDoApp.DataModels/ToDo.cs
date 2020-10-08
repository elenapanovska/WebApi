using System.Collections.Generic;

namespace Homework.WebApi.ToDoApp.DataModels
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; } = false;
        public IEnumerable<SubTask> SubTasks { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}