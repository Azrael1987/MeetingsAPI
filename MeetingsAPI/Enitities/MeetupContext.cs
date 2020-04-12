using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI.Enitities
{
    public class MeetupContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-MSTE5ED\\SQLEXPRESS;Database=MeetingsDb;Trusted_Connection=True;";

        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<SpecialGuest> SpecialGuests { get; set; }
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

            // koncepcja Marka Z.
            modelBuilder.Entity<SpecialGuestJoint>()
                .HasKey(j => new {j.MeetupId, j.FirstSpecialGuestId, j.SecondSpecialGuestId});

            modelBuilder.Entity<SpecialGuestJoint>()
                .HasOne(m => m.Meetups)
                .WithMany(l => l.SpecialGuestJoints)
                .HasForeignKey(l => l.MeetupId)
                .OnDelete(DeleteBehavior.Cascade); // usuwanie kaskadowe !

            modelBuilder.Entity<SpecialGuestJoint>()
                .HasOne(m => m.FirstSpecialGuests)
                .WithMany(l => l.FirstSpecialGuestJoints)
                .HasForeignKey(l => l.FirstSpecialGuestId)
                .OnDelete(DeleteBehavior.Restrict); // usuwnanie nie kaskadowe

            modelBuilder.Entity<SpecialGuestJoint>()
                .HasOne(m => m.SecondSpecialGuests)
                .WithMany(j => j.SecondSpecialGuestJoints)
                .HasForeignKey(m => m.SecondSpecialGuestId)
                .OnDelete(DeleteBehavior.Restrict); // usuwnanie nie kaskadowe
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
