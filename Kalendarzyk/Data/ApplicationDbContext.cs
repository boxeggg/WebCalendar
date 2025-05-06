using Kalendarzyk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kalendarzyk.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LocationModel>()
                .HasOne(l => l.User)
                    .WithMany(u => u.Locations)
                        .HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EventModel>()
                .HasOne(e => e.User)
                    .WithMany(u => u.Events)
                        .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EventModel>()
                .HasOne(e => e.Location)
                    .WithMany(l => l.Events)
                        .HasForeignKey(e => e.LocationId).OnDelete(DeleteBehavior.Restrict);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<UserModel> UserModel { get; set; }

    }
}
