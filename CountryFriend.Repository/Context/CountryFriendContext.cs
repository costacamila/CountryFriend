using CountryFriend.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CountryFriend.Repository.Context
{
    public class CountryFriendContext : DbContext
    {
        public DbSet<Domain.Country.Country> Countries { get; set; }
        public DbSet<Domain.Friend.Friend> Friends { get; set; }
        public DbSet<Domain.Friend.FriendFriend> FriendFriends { get; set; }
        public DbSet<Domain.State.State> States { get; set; }
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public CountryFriendContext(DbContextOptions<CountryFriendContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryMap());
            modelBuilder.ApplyConfiguration(new FriendMap());
            modelBuilder.ApplyConfiguration(new FriendFriendMap());
            modelBuilder.ApplyConfiguration(new StateMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
