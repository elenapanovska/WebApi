using AutoMapper;
using Homework.WebApi.ToDoApp.DataModels;
using Homework.WebApi.ToDoApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, LoggedInUser>();
            CreateMap<RegisterUser, User>();
            CreateMap<ToDo, ToDoDto>();
            CreateMap<SubTask, SubTaskDto>().ReverseMap();
        }
    }
}
