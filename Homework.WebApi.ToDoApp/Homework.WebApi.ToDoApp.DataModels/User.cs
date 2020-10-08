using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<ToDo> ToDoTasks { get; set; }
    }
}
