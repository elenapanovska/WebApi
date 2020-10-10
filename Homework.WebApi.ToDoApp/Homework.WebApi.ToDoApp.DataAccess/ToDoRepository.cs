using Homework.WebApi.ToDoApp.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.WebApi.ToDoApp.DataAccess
{
    public class ToDoRepository : IRepository<ToDo>
    {
        private readonly TasksDbContext _context;

        public ToDoRepository(TasksDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDo> GetAll()
        {
            return _context.ToDoTasks.Include(x => x.SubTasks);
        }

        public ToDo GetById(int id)
        {
            return _context.ToDoTasks.FirstOrDefault(t => t.Id == id);
        }

        public void Insert(ToDo entity)
        {
            _context.ToDoTasks.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ToDo entity)
        {
            _context.ToDoTasks.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(ToDo entity)
        {
            _context.ToDoTasks.Remove(entity);
            _context.SaveChanges();
        }
    }
}
