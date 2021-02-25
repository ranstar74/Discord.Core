using Discord.Core.Entities.Tables;
using Microsoft.EntityFrameworkCore;

namespace Discord.Core.Entities
{
    public class DiscordDb : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<GroupsToUsers> GroupsToUsers { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }

        public DiscordDb()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupsToUsers>().HasKey(k => new { k.UserId, k.GroupId });

            modelBuilder.Entity<GroupsToUsers>()
                .HasOne(x => x.User)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<GroupsToUsers>()
               .HasOne(x => x.Group)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.GroupId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
                "Data Source=128.75.146.65,1433;" +
                "Database=Discord;" +
                "User ID=ranstar74;" +
                "Password=9%YyG46TN^Eu)\"Yx#G@!w\"b");
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            //    "Server=localhost\\SQLEXPRESS;Database=Discord;Trusted_Connection=True;");
        }
    }
}
