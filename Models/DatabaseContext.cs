using Microsoft.EntityFrameworkCore;

namespace PollSignalR.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Poll>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Option>()
                .Property(e => e.VoteCount)
                .HasDefaultValue(0);
        }
    }
}