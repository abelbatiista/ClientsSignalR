using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services.DrinkService
{
    public class CDrinkService : IService<Drink>
    {

        private readonly C6_PP_T12Context _context;

        public CDrinkService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Drink> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Drink> FindByName(string name)
        {
            try
            {
                return await _context.Drinks.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Drink>> GetAll()
        {
            try
            {
                return await _context.Drinks.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Drink> GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Drink item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Drink item)
        {
            throw new NotImplementedException();
        }
    }
}
