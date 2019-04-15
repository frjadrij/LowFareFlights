using LowFareFlights.Dal.Repositories.Generic;
using LowFareFlights.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.Repositories.Concrete
{
    public interface IFlightRepository : IBaseRepository<Flight>
    {
    }
}
