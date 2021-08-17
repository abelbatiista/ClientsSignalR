using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services.MainPlateService
{
    public class CMainPlateService : IService<MainPlate>
    {

        private readonly C6_PP_T12Context _context;

        public CMainPlateService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MainPlate> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MainPlate> FindByName(string name)
        {
            try
            {
                return await _context.MainPlates.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<MainPlate>> GetAll()
        {
            try
            {
                return await _context.MainPlates.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<MainPlate> GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(MainPlate item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(MainPlate item)
        {
            throw new NotImplementedException();
        }
    }
}
