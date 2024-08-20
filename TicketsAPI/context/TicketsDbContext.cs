using Microsoft.EntityFrameworkCore;
using TicketsAPI.model;

namespace TicketsAPI.context
{
    class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Provider> Providers => Set<Provider>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Provider>()
                .HasData(
                    new Provider { Id = 1, Name = "Contoso" },
                    new Provider { Id = 2, Name = "Fortune hunters" }
                );

            modelBuilder.Entity<Ticket>()
                .HasData(
                    new Ticket { Id = 1, Name = "Concert Ticket - Front Row", Quantity = 100, ProviderId = 1 },
                    new Ticket { Id = 2, Name = "Movie Ticket - Premium", Quantity = 200, ProviderId = 1 },
                    new Ticket { Id = 3, Name = "Theater Ticket - Balcony", Quantity = 50, ProviderId = 1 },
                    new Ticket { Id = 4, Name = "Sports Event Ticket - General Admission", Quantity = 150, ProviderId = 2 },
                    new Ticket { Id = 5, Name = "Workshop Ticket - Full Day", Quantity = 75, ProviderId = 2 },
                    new Ticket { Id = 6, Name = "Conference Pass - Standard", Quantity = 120, ProviderId = 2 },
                    new Ticket { Id = 7, Name = "Exhibition Entry - VIP", Quantity = 30, ProviderId = 1 },
                    new Ticket { Id = 8, Name = "Webinar Ticket - Basic", Quantity = 250, ProviderId = 1 },
                    new Ticket { Id = 9, Name = "Festival Ticket - Weekend Pass", Quantity = 80, ProviderId = 2 },
                    new Ticket { Id = 10, Name = "Tour Ticket - City Sightseeing", Quantity = 40, ProviderId = 2 },
                    new Ticket { Id = 11, Name = "Training Session - Advanced", Quantity = 60, ProviderId = 1 },
                    new Ticket { Id = 12, Name = "Annual Membership - Premium", Quantity = 90, ProviderId = 2 }
                );

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Provider)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProviderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
