using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services.StarterService
{
    public class CStarterService : IService<Starter>
    {

        private readonly C6_PP_T12Context _context;

        public CStarterService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Starter> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Starter> FindByName(string name)
        {
            try
            {
                return await _context.Starters.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Starter>> GetAll()
        {
            try
            {
                return await _context.Starters.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Starter> GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Starter item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Starter item)
        {
            throw new NotImplementedException();
        }
    }
}
