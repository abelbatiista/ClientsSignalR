using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services
{
    interface IService<T>
    {
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(int id);
        Task<T> FindById(int id);
        Task<List<T>> GetAll();
        Task<T> GetLast();
        Task<T> FindByName(string name);
    }
}
