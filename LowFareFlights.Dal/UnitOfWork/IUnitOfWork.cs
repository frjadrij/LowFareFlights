using LowFareFlights.Dal.DatabaseContext;
using LowFareFlights.Dal.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.UnitOfWork
{
    /// <summary>
    /// Unit of Work pattern interface.
    /// </summary>
    public interface IUnitOfWork
    {
        LowFareFlightsContext LowFareFlightsContext { get; }

        IFlightRepository Flights { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
