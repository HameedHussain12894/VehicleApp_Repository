using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Dal.Infrastructure.IRepository;
using Vehicle.Model;

namespace Vehicle.Dal.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVehicleRepository vehicle { get; private set; }

        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            vehicle = new VehicleRepository(_context);
        }

        public  void Save()
        {
           _context.SaveChanges();
        }
    }
}
