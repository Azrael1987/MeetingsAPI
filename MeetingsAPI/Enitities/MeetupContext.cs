using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI.Enitities
{
    public class MeetupContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-MSTE5ED\\SQLEXPRESS;Database=MeetingsDb;Trusted_Connection=True;";

        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<SpecialGuest> SpecialGuests { get; set;}
        public DbSet<SpecialGuestJoint> SpecialGuestJoint { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meetup>()
                .HasOne(m => m.Location)
                .WithOne(l => l.Meetup)
                .HasForeignKey<Location>(l => l.MeetupId);

            modelBuilder.Entity<Meetup>()
                .HasMany(m => m.Lectures)
                .WithOne(l => l.Meetup);

            modelBuilder.Entity<SpecialGuestJoint>()
                .HasMany(m => m.Meetups)
                .WithOne(s => s.SpecialGuestJoint);

            modelBuilder.Entity<SpecialGuest>()
                .HasMany(s => s.SpecialGuestJoints)
                .WithOne(s => s.FirstSpecialGuest);

            modelBuilder .Entity<SpecialGuest>()
                .HasMany(s => s.SpecialGuestJoints)
                .WithOne(s => s.SecondSpecialGuest);
        }

        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
