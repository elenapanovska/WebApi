using Homework.WebApi.ToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Interfaces
{
    public interface IUserService
    {
        LoggedInUser LogIn(string username, string password);
        void Register(RegisterUser userRequest);
    }
}
