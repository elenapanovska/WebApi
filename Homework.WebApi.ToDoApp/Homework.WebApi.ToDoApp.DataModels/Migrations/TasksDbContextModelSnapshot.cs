﻿// <auto-generated />
using Homework.WebApi.ToDoApp.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Homework.WebApi.ToDoApp.DataModels.Migrations
{
    [DbContext(typeof(TasksDbContext))]
    partial class TasksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Homework.WebApi.ToDoApp.DataModels.SubTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDone");

                    b.Property<string>("Name");

                    b.Property<int>("ToDoTaskId");

                    b.HasKey("Id");

                    b.HasIndex("ToDoTaskId");

                    b.ToTable("SubTask");

                    b.HasData(
                        new { Id = 1, IsDone = false, Name = "Learn WebApi first", ToDoTaskId = 1 }
                    );
                });

            modelBuilder.Entity("Homework.WebApi.ToDoApp.DataModels.ToDo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDone");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ToDo");

                    b.HasData(
                        new { Id = 1, IsDone = false, Name = "Finish WebApi homework", UserId = 1 }
                    );
                });

            modelBuilder.Entity("Homework.WebApi.ToDoApp.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = 1, FirstName = "Bob", LastName = "Bobsky", Password = "e?Dm~???J-;}??", Username = "bobB123" }
                    );
                });

            modelBuilder.Entity("Homework.WebApi.ToDoApp.DataModels.SubTask", b =>
                {
                    b.HasOne("Homework.WebApi.ToDoApp.DataModels.ToDo", "ToDoTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ToDoTaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Homework.WebApi.ToDoApp.DataModels.ToDo", b =>
                {
                    b.HasOne("Homework.WebApi.ToDoApp.DataModels.User", "User")
                        .WithMany("ToDoTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}