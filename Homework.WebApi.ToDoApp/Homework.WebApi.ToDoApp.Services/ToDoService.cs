using AutoMapper;
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
        private readonly IMapper _mapper;

        public ToDoService(IRepository<ToDo> toDoRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<ToDoDto> GetUserToDos(int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);
            
            if(user == null)
            {
                throw new CustomException(null, null, "User not found!");
            }

            var toDos = _mapper.Map<List<ToDoDto>>(user.ToDoTasks);

            return toDos;
        }

        public ToDoDto GetToDo(int toDoId, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new CustomException(null, null, "User not found!");
            }

            var toDo = user.ToDoTasks.FirstOrDefault(t => t.Id == toDoId);

            if (toDo == null)
            {
                throw new CustomException(null, toDo.Name, "Note does not exist!");
            }

            var toDoDto = _mapper.Map<ToDoDto>(toDo);

            return toDoDto;
        }

        public void AddToDo(ToDoDto toDoRequest, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new CustomException(null, null, "User not found!");
            };

            if(toDoRequest == null)
            {
                throw new CustomException(null);
            }

            if (string.IsNullOrWhiteSpace(toDoRequest.Name))
            {
                throw new CustomException(toDoRequest.Id, toDoRequest.Name, "Name is requried!");
            }

            var subtasks = _mapper.Map<IEnumerable<SubTask>>(toDoRequest.SubTasks);

            var todo = new ToDo
            {
                Name = toDoRequest.Name,
                IsDone = toDoRequest.IsDone,
                UserId = userId,
                User = user,
                SubTasks = subtasks
            };

            user.ToDoTasks.ToList().Add(todo);
            _toDoRepository.Insert(todo);
        }

        public void DeleteToDo(int toDoId, int userId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new CustomException(null, null, "User not found!");
            }

            var toDo = user.ToDoTasks.FirstOrDefault(t => t.Id == toDoId);

            if (toDo == null)
            {
                throw new CustomException(null, toDo.Name, "Note does not exist!");
            };
           
            user.ToDoTasks.ToList().Remove(toDo);
            _toDoRepository.Delete(toDo);
        }
    }
}
