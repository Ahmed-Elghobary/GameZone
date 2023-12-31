﻿using GameZone.Models;

namespace GameZone.Data
{
    public class GameDbContext:DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options):base(options) 
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Name="Sports"},
                new Category { Id = 2, Name="Action"},
                new Category { Id = 3, Name="Adventure"},
                new Category { Id = 4, Name="Racing"},
                new Category { Id = 5, Name="Fighting"},
                new Category { Id = 6, Name="Film"}
            });

            modelBuilder.Entity<Device>().HasData(

                new Device[]
                {
                    new Device { Id = 1,Name="PlayStaion",Icon="bi bi-playstation"},
                    new Device { Id = 2,Name="xbox",Icon="bi bi-xbox"},
                    new Device { Id = 3,Name="Nintendo",Icon="bi nintendo-switch"},
                    new Device { Id = 4,Name="PC",Icon="bi bi-pc-display"}
                });

            modelBuilder.Entity<GameDevice>().HasOne(x => x.Device).
                WithMany(x => x.GameDevices)
                .HasForeignKey(x => x.DeviceId);

            modelBuilder.Entity<GameDevice>().HasOne(x=>x.Game)
                .WithMany(x=>x.Devices)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<GameDevice>().HasKey(x => new { x.DeviceId, x.GameId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
