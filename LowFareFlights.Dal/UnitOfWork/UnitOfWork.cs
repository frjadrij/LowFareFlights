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
    /// Unit of Work pattern concrete class.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public LowFareFlightsContext LowFareFlightsContext { get; private set; }

        public IFlightRepository Flights { get; private set;  }

        public UnitOfWork()
        {
            LowFareFlightsContext = new LowFareFlightsContext();
            Flights = new FlightRepository(LowFareFlightsContext);
        }

        public int SaveChanges() => LowFareFlightsContext.SaveChanges();
        public async Task<int> SaveChangesAsync() => await LowFareFlightsContext.SaveChangesAsync();
    }
}
