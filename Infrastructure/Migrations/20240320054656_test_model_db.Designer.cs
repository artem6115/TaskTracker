﻿// <auto-generated />
using System;
using Infrastructure.Utilits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TaskTrackerDbContext))]
    [Migration("20240320054656_test_model_db")]
    partial class test_model_db
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Auth.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFaildCount")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserBanned")
                        .HasColumnType("bit");

                    b.Property<bool>("UserConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("UserDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Infrastructure.Entities.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("DateOfCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfEdit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Infrastructure.Entities.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Infrastructure.Entities.ProjectTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProjectTaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTaskId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Infrastructure.Entities.StatusTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<long>("ProjectTaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTaskId");

                    b.ToTable("StatusTasks");
                });

            modelBuilder.Entity("Infrastructure.Auth.User", b =>
                {
                    b.HasOne("Infrastructure.Entities.Project", null)
                        .WithMany("Users")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Infrastructure.Entities.Project", b =>
                {
                    b.HasOne("Infrastructure.Auth.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Infrastructure.Entities.ProjectTask", b =>
                {
                    b.HasOne("Infrastructure.Entities.Project", "Project")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.ProjectTask", null)
                        .WithMany("InternalTasks")
                        .HasForeignKey("ProjectTaskId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Infrastructure.Entities.StatusTask", b =>
                {
                    b.HasOne("Infrastructure.Entities.ProjectTask", "ProjectTask")
                        .WithMany("StatusTask")
                        .HasForeignKey("ProjectTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectTask");
                });

            modelBuilder.Entity("Infrastructure.Entities.Project", b =>
                {
                    b.Navigation("ProjectTasks");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Infrastructure.Entities.ProjectTask", b =>
                {
                    b.Navigation("InternalTasks");

                    b.Navigation("StatusTask");
                });
#pragma warning restore 612, 618
        }
    }
}