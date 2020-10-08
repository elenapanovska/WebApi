using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services
{
    public class UserException : Exception
    {
        public UserException(int? userId, string name)
            :base("Something wrong with the user")
        {
        }

        public UserException(int? userId, string name, string message)
            :base(message)
        {
        }
    }
}
