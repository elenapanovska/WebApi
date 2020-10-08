using Homework.WebApi.ToDoApp.DataAccess;
using Homework.WebApi.ToDoApp.DataModels;
using Homework.WebApi.ToDoApp.Models;
using Homework.WebApi.ToDoApp.Services.Exceptions;
using Homework.WebApi.ToDoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<ToDo> _toDoRepository;
        private readonly IRepository<User> _userRepository;

        public ToDoService(IRepository<ToDo> toDoRepository, IRepository<User> userRepository)
        {
            _toDoRepository = toDoRepository;
            _userRepository = userRepository;
        }

        public List<ToDoDto> GetUserToDos(int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            
            if(user == null)
            {
                throw new ToDoException(null, null, "User not found!");
            }

            var toDos = new List<ToDoDto>();

            foreach (var toDo in user.ToDoTasks)
            {
                var toDoDto = new ToDoDto
                {
                    Id = toDo.Id,
                    Name = toDo.Name,
                    IsDone = toDo.IsDone,
                    SubTasks = new List<SubTaskDto>()
                };
                toDos.Add(toDoDto);
            };

            return toDos;
        }

        public ToDoDto GetToDo(int toDoId, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ToDoException(null, null, "User not found!");
            }

            var toDo = user.ToDoTasks.FirstOrDefault(t => t.Id == toDoId);

            if (toDo == null)
            {
                throw new ToDoException(null, toDo.Name, "Note does not exist!");
            }

            var toDoDto = new ToDoDto
            {
                Id = toDo.Id,
                Name = toDo.Name,
                IsDone = toDo.IsDone,
                SubTasks = new List<SubTaskDto>()
            };

            return toDoDto;
        }

        public void AddToDo(ToDoDto toDoRequest, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ToDoException(null, null, "User not found!");
            };

            if(toDoRequest == null)
            {
                throw new ToDoException(null);
            }

            if (string.IsNullOrWhiteSpace(toDoRequest.Name))
            {
                throw new ToDoException(toDoRequest.Id, toDoRequest.Name, "Name is requried!");
            }

            var todo = new ToDo
            {
                Name = toDoRequest.Name,
                IsDone = toDoRequest.IsDone,
                UserId = userId,
                User = user
            };

            user.ToDoTasks.ToList().Add(todo);
            _toDoRepository.Insert(todo);
        }

        public void DeleteToDo(int toDoId, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ToDoException(null, null, "User not found!");
            }

            var toDo = user.ToDoTasks.FirstOrDefault(t => t.Id == toDoId);

            if (toDo == null)
            {
                throw new ToDoException(null, toDo.Name, "Note does not exist!");
            };
           
            user.ToDoTasks.ToList().Remove(toDo);
            _toDoRepository.Delete(toDo);
        }
    }
}
