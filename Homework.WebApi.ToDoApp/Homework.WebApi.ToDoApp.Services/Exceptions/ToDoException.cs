using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Exceptions
{
    public class ToDoException : Exception
    {
        public ToDoException(string name)
         : base("Something went wrong")
        {
        }

        public ToDoException(int? noteId, string name, string message)
            : base(message)
        {
        }
    }
}
