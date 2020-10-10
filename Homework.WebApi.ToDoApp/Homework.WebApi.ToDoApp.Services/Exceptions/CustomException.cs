using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string name)
         : base("Something went wrong")
        {
        }

        public CustomException(int? Id, string name, string message)
            : base(message)
        {
        }
    }
}
