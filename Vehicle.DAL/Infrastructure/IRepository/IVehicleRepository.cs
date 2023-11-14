using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model;

namespace Vehicle.Dal.Infrastructure.IRepository
{
    public interface IVehicleRepository:IRepository<vehicle>
    {
        void Add(vehicle vehicles);

        void Update(vehicle _vehicle);

        List<vehicle> SearchVehicles(string searchTerm);


    }
}
