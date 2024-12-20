using Microsoft.EntityFrameworkCore;

namespace TrafficLight.Infrastructure;

public class TrafficLightContext(DbContextOptions<TrafficLightContext> options) : DbContext(options)
{
    public DbSet<Domain.TrafficLight> TrafficLights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TrafficLightDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.TrafficLight>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Domain.TrafficLight>()
            .Property(t => t.Color)
            .IsRequired();
    }
}
