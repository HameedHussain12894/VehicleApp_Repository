using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Dal.Infrastructure.IRepository;
using Vehicle.Model;
using EFCore.BulkExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Vehicle.Dal.Infrastructure.Repository
{
    public class VehicleRepository : Repository<vehicle>, IVehicleRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<vehicle> _vehicles;
        public List<vehicle> listofvehicles { get; set; }
        public VehicleRepository(ApplicationDBContext context) : base(context)
        {
            _context=context;
            _vehicles=_context.Set<vehicle>();
        }

        public async void Add(vehicle vehicles)
        {
             await _vehicles.AddAsync(vehicles);
        }

        public List<vehicle> SearchVehicles(string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return listofvehicles;
            }
            return listofvehicles.Where(x=>x.RegNo.Contains(searchTerm) || x.Color.Contains(searchTerm) || x.Model.Contains(searchTerm)).ToList();
        }

        public void Update(vehicle _vehicle)
        {
            _vehicles.Update(_vehicle);
        }
    }
}
