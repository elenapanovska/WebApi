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
    public class SubTaskService : ISubTaskService
    {
        private readonly IRepository<SubTask> _subTaskRespository;
        private readonly IMapper _mapper;

        public SubTaskService(IRepository<SubTask> subTaskRespository,  IMapper mapper)
        {
            _subTaskRespository = subTaskRespository;
            _mapper = mapper;
        }

        public void AddSubTask(SubTaskDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new CustomException(request.Id, request.Name, "Name is requried!");
            };

            var subTask = _mapper.Map<SubTask>(request);

            _subTaskRespository.Insert(subTask);
        }

        public void DeleteSubTask(int id)
        {
            var subTask = _subTaskRespository.GetAll().FirstOrDefault(s => s.Id == id);

            if(subTask == null)
            {
                throw new CustomException(id, null, "No sub task was found!");
            };

            _subTaskRespository.Delete(subTask);
        }
    }
}
