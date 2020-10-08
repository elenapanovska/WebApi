using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.WebApi.ToDoApp.DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Insert(T entity);
        void Delete(T entity);

    }
}
