using Homework.WebApi.ToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Interfaces
{
    public interface ISubTaskService
    {
        void AddSubTask(SubTaskDto request);
        void DeleteSubTask(int id);
    }
}
