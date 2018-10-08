using Microsoft.EntityFrameworkCore;

namespace TrainTracker.Models{
    public class Context : DbContext{
        public Context(DbContextOptions<Context> options) : base(options){}
        
        public DbSet<TrainStation> TrainStations{get; set;}
        public DbSet<TrainStationMin> TrainStationsMin{get; set;}

        public DbSet<Tip> Tips { get; set; }
    }
}