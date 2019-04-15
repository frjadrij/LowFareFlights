using LowFareFlights.Entity.Configurations;
using LowFareFlights.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.DatabaseContext
{
    /// <summary>
    /// Personalized DbContext with Db initialization strategy, Code First Db tables configuration and customized Db change operations.
    /// </summary>
    public class LowFareFlightsContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }

        /// <summary>
        /// Static method for getting Connection string from Web.config
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var connectionstring = ConfigurationManager.ConnectionStrings["LowFareFlightsConnectionString"].ConnectionString;
                return connectionstring;
            }
        }

        /// <summary>
        /// Customised DbContext constructor with Db initialization strategy 
        /// </summary>
        public LowFareFlightsContext() : base(ConnectionString)
        {
            Database.SetInitializer<LowFareFlightsContext>(new CreateDatabaseIfNotExists<LowFareFlightsContext>());
        }

        /// <summary>
        /// Customized DbContext.OnModelCreating(DbModelBuilder modelBuilder) for Db tables configuration.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new FlightEntityConfiguration());
        }
    }
}
