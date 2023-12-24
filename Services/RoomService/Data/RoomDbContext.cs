using Microsoft.EntityFrameworkCore;
using RoomService.Entityty;
using SmartTicket.Infrastructure.Persistence;
using SmartTicket.Infrastructure.Services;

namespace RoomService.Data
{
    public class RoomDbContext : BaseDbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options, IUserContext userContext) : base(options, userContext)
        {
        }

        public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options)
        {
        }

        public DbSet<RoomAvailability> RoomAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomAvailability).Assembly);

        }
    }
}
