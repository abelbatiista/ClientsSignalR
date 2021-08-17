using Database.Models;
using LogicBusiness.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Services.RestaurantOrderService
{
    public class CRestaurantOrderService : IService<RestaurantOrder>
    {

        private readonly C6_PP_T12Context _context;

        public CRestaurantOrderService()
        {
            _context = new C6_PP_T12Context();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RestaurantOrder> FindById(int id)
        {
            try
            {
                return await _context.RestaurantOrders.FirstOrDefaultAsync(x => x.RestaurantOrderId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<RestaurantOrder> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<RestaurantOrder>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RestaurantOrder> GetLast()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(RestaurantOrder item)
        {
            try
            {
                _context.RestaurantOrders.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> Update(RestaurantOrder item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<COrdersViewModel>> GetAllViewModel()
        {
            try
            {
                return await _context.RestaurantOrders.Select(s => new COrdersViewModel
                {
                    Codigo = s.RestaurantOrderId,
                    Cliente = s.Client.Name,
                    FechaHora = s.Date,
                    Subtotal = s.Subtotal,
                    ITBIS = s.Itbis,
                    Total = s.Total,
                    Estado = s.Status.Name
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<COrderDetailsViewModel> GetDetailViewModel(int id)
        {
            try
            {
                return await _context.RestaurantOrders
                    .Select(s => new COrderDetailsViewModel
                    {
                        Codigo = s.RestaurantOrderId,
                        Cliente = s.Client.Name,
                        Entrada = s.Starter.Name,
                        Principal = s.MainPlate.Name,
                        Postre = s.Dessert.Name,
                        Bebida = s.Drink.Name,
                        FechaHora = s.Date,
                        Subtotal = s.Subtotal,
                        ITBIS = s.Itbis,
                        Total = s.Total,
                        Estado = s.Status.Name
                    })
                    .FirstOrDefaultAsync(c => c.Codigo == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> ChangeOrderStatus(RestaurantOrder item)
        {
            try
            {
                var entity = await _context.RestaurantOrders.FirstOrDefaultAsync(x => x.RestaurantOrderId == item.RestaurantOrderId);

                if (entity == null)
                {
                    return false;
                }

                _context.Entry(entity).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
