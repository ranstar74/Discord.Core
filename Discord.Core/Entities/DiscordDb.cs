﻿using Discord.Core.Entities.Tables;
using Microsoft.EntityFrameworkCore;

namespace Discord.Core.Entities
{
    public class DiscordDb : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<ServersToUsers> ServersToUsers { get; set; }
        public virtual DbSet<Servers> Servers { get; set; }

        public DiscordDb()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServersToUsers>().HasKey(k => new { k.UserId, k.ServerId });

            modelBuilder.Entity<ServersToUsers>()
                .HasOne(x => x.User)
                .WithMany(x => x.Servers)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ServersToUsers>()
               .HasOne(x => x.Server)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.ServerId);

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
