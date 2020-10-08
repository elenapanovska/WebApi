using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Homework.WebApi.ToDoApp.DataModels
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDoTasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User model configurations
            modelBuilder
                .Entity<User>()
                .ToTable(nameof(User))
                .HasKey(k => k.Id);

            modelBuilder
                .Entity<User>()
                .Property(p => p.Username)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder
                .Entity<User>()
                .Property(p => p.Password)
                .HasMaxLength(20)
                .IsRequired();

            // ToDo model configurations
            modelBuilder
                .Entity<ToDo>()
                .ToTable(nameof(ToDo))
                .HasKey(k => k.Id);

            modelBuilder
                .Entity<ToDo>()
                .HasOne(p => p.User)
                .WithMany(p => p.ToDoTasks)
                .HasForeignKey(k => k.UserId);

            // SubTask model configurations
            modelBuilder
                .Entity<SubTask>()
                .ToTable(nameof(SubTask))
                .HasKey(k => k.Id);

            modelBuilder
                .Entity<SubTask>()
                .HasOne(p => p.ToDoTask)
                .WithMany(p => p.SubTasks)
                .HasForeignKey(k => k.ToDoTaskId);

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public void Seed(ModelBuilder modelBuilder)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("todoApp12345"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bobB123",
                    Password = hashedPassword
                });

            modelBuilder.Entity<ToDo>()
                .HasData(
                new ToDo()
                {
                    Id = 1,
                    Name ="Finish WebApi homework",
                    UserId=1
                });

            modelBuilder.Entity<SubTask>()
                .HasData(
                new SubTask()
                {
                    Id = 1,
                    Name = "Learn WebApi first",
                    ToDoTaskId = 1
                });
        }

    }
}
