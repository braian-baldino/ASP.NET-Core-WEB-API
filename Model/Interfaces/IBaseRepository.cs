using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        Task<List<T>> GetAll(int userId);
        Task<T> Get(int id,int userId);
        Task<T> Add(T entity,int userId);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<User> ValidUser(int userId);
        bool Exist(int id);

    }
}
