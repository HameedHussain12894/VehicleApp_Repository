using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Dal.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        IVehicleRepository vehicle { get; }

        void Save();
    }
}
