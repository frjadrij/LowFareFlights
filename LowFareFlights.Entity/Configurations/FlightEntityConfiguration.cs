using LowFareFlights.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Entity.Configurations
{
    public class FlightEntityConfiguration : EntityTypeConfiguration<Flight>
    {
        public FlightEntityConfiguration()
        {
            HasKey(e => e.Id);
        }
    }
}
