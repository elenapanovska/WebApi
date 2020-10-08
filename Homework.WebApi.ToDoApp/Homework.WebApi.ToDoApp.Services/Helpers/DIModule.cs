using Homework.WebApi.ToDoApp.DataAccess;
using Homework.WebApi.ToDoApp.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.Services.Helpers
{
    public static class DIModule
    {
        public static  IServiceCollection RegisterModule(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TasksDbContext>(c => c.UseSqlServer(connectionString));

            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<ToDo>, ToDoRepository>();
            services.AddTransient<IRepository<SubTask>, SubTaskRepository>();

            return services;
        }
    }
}
