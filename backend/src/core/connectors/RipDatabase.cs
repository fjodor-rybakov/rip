using System;
using System.Collections.Generic;
using backend.models.assets;
using backend.models.entities;
using Microsoft.EntityFrameworkCore;

namespace backend.core.connectors
{
    public sealed class RipDatabase : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<CommentEntity> Comment { get; set; }

        public RipDatabase()
        {
            Database.EnsureCreated();
            Console.WriteLine("Database connection was set!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity {Id = 1, RoleName = AcceptRole.Administrator}, new RoleEntity {Id = 2, RoleName = AcceptRole.User}
            );
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Avatar)
                .IsUnique();
            modelBuilder.Entity<UserEntity>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
            modelBuilder.Entity<UserEntity>().Property(b => b.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<UserEntity>().Property(b => b.UpdatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<RoleEntity>().Property(b => b.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<RoleEntity>().Property(b => b.UpdatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<NewsEntity>().Property(b => b.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<NewsEntity>().Property(b => b.UpdatedAt).HasDefaultValueSql("NOW()");
            
            modelBuilder.Entity<CommentEntity>().Property(b => b.CreatedAt).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<CommentEntity>().Property(b => b.UpdatedAt).HasDefaultValueSql("NOW()");
            
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