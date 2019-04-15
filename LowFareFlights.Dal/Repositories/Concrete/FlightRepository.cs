using LowFareFlights.Dal.DatabaseContext;
using LowFareFlights.Dal.Repositories.Generic;
using LowFareFlights.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.Repositories.Concrete
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {

        /// <summary>
        /// Concrete repository constructor with customised DbContext.
        /// </summary>
        /// <param name="dbContext">
        public FlightRepository(LowFareFlightsContext dbContext) : base(dbContext) { }

        /// <summary>
        /// DbContext getter with boxing to HRMSContext. 
        /// </summary>
        public LowFareFlightsContext LowFareFlightsContext => _dbContext as LowFareFlightsContext;
    }
}
