using System;
using backend.models.assets;
using backend.models.entities;
using Microsoft.EntityFrameworkCore;

namespace backend.core.connectors
{
    public sealed class RipDatabase : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public RipDatabase()
        {
            Database.EnsureCreated();
            Console.WriteLine("Database connection was set!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity() {Id = 1, RoleName = AcceptRole.Administrator}, new RoleEntity {Id = 2, RoleName = AcceptRole.User}
            );
            modelBuilder.Entity<UserEntity>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
            base.OnModelCreating(modelBuilder);
            Console.WriteLine("Models was updated!");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                Environment.GetEnvironmentVariable("DB_CONNECTOR_STRING") ??
                throw new Exception("Env variable DB_CONNECTOR_STRING not found")
            );
    }
}