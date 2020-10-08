using Homework.WebApi.ToDoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework.WebApi.ToDoApp.DataAccess
{
    public class SubTaskRepository : IRepository<SubTask>
    {
        private readonly TasksDbContext _context;

        public SubTaskRepository(TasksDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SubTask> GetAll()
        {
            return _context.SubTasks;
        }

        public SubTask GetById(int id)
        {
            return _context.SubTasks.FirstOrDefault(s => s.Id == id);
        }

        public void Insert(SubTask entity)
        {
            _context.SubTasks.Add(entity);
            _context.SaveChanges();
        }

        public void Update(SubTask entity)
        {
            _context.SubTasks.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(SubTask entity)
        {
            _context.SubTasks.Remove(entity);
            _context.SaveChanges();
        }

    }
}
