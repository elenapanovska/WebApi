using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Models
{
    public class LoggedInUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
