using Homework.WebApi.ToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Interfaces
{
    public interface IToDoService
    {
        List<ToDoDto> GetUserToDos(int userId);
        ToDoDto GetToDo(int toDoId, int userId);
        void AddToDo(ToDoDto toDoRequest, int userId);
        void DeleteToDo(int toDoId, int userId);
    }
}
