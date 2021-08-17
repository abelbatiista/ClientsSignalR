using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LogicBusiness.Services.ClientService
{
    public class CClientService : IService<Client>
    {

        private readonly C6_PP_T12Context _context;

        public CClientService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> FindByName(string name)
        {
            try
            {
                return await _context.Clients.FirstOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<Client>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Client> GetLast()
        {
            try
            {
                return await _context.Clients.OrderByDescending(x => x.ClientId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Insert(Client item)
        {
            try
            {
                _context.Clients.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> Update(Client item)
        {
            throw new NotImplementedException();
        }
    }
}
