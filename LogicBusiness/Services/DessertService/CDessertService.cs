using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services.DessertService
{
    public class CDessertService : IService<Dessert>
    {

        private readonly C6_PP_T12Context _context;

        public CDessertService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Dessert> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Dessert> FindByName(string name)
        {
            try
            {
                return await _context.Desserts.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Dessert>> GetAll()
        {
            try
            {
                return await _context.Desserts.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Dessert> GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Dessert item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Dessert item)
        {
            throw new NotImplementedException();
        }
    }
}
