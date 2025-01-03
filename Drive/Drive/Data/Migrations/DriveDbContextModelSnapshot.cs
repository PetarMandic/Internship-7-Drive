﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Drive.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drive.Data.Migrations
{
    [DbContext(typeof(DriveDbContext))]
    partial class DriveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Drive.Data.Entities.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Comments")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EditingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FolderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<List<string>>("Text")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string[]>("UsersIds")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("UserId");

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0565805d-44eb-4a31-8e97-7649a6c09152"),
                            EditingTime = new DateTime(2024, 12, 24, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(579),
                            Name = "Document1.txt",
                            Text = new List<string> { "dokument1" },
                            UserId = new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"),
                            UsersIds = new[] { "7f9cf05b-a16d-477e-8942-15f55d9b758c" }
                        },
                        new
                        {
                            Id = new Guid("21a9f299-00d7-411d-941f-d3c095e62513"),
                            EditingTime = new DateTime(2024, 12, 29, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(593),
                            Name = "Image1.jpg",
                            Text = new List<string> { "najbolja slika" },
                            UserId = new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"),
                            UsersIds = new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" }
                        },
                        new
                        {
                            Id = new Guid("d9b81b94-111d-4ea8-846e-45e9f824630a"),
                            EditingTime = new DateTime(2025, 1, 1, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(599),
                            Name = "ProjectProposal.docx",
                            Text = new List<string> { "prvi projekt" },
                            UserId = new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"),
                            UsersIds = new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" }
                        },
                        new
                        {
                            Id = new Guid("ec404ca9-9817-4513-927f-7f07c2f31de3"),
                            EditingTime = new DateTime(2024, 12, 26, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(605),
                            Name = "Document2.pdf",
                            Text = new List<string> { "dokument2" },
                            UserId = new Guid("0375d7fb-6dd2-44ad-ae74-fbd6e193ee7e"),
                            UsersIds = new[] { "21e9d0c3-3e7f-4811-b22e-06917be2d113" }
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid?>("ParentFolderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string[]>("UsersIds")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("ParentFolderId");

                    b.HasIndex("UserId");

                    b.ToTable("Folders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("91cd1709-314b-4bc8-b4b7-01976afa7a6e"),
                            Name = "Documents",
                            UserId = new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"),
                            UsersIds = new[] { "7f9cf05b-a16d-477e-8942-15f55d9b758c" }
                        },
                        new
                        {
                            Id = new Guid("67ca63e3-88f1-46a4-90a2-6ef86d89cf1c"),
                            Name = "Images",
                            UserId = new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"),
                            UsersIds = new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" }
                        },
                        new
                        {
                            Id = new Guid("0708d48e-85d8-4d50-9ff4-47aa7d925324"),
                            Name = "Projects",
                            UserId = new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"),
                            UsersIds = new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" }
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"),
                            Mail = "bartol.deak@example.com",
                            Password = "password123"
                        },
                        new
                        {
                            Id = new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"),
                            Mail = "ante.roca@example.com",
                            Password = "password456"
                        },
                        new
                        {
                            Id = new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"),
                            Mail = "matija.luketin@example.com",
                            Password = "password789"
                        },
                        new
                        {
                            Id = new Guid("0375d7fb-6dd2-44ad-ae74-fbd6e193ee7e"),
                            Mail = "duje.saric@example.com",
                            Password = "password101"
                        },
                        new
                        {
                            Id = new Guid("b5364602-c06c-4f62-aeee-a74991543a27"),
                            Mail = "marija.sustic@example.com",
                            Password = "password102"
                        });
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Comment", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.File", "File")
                        .WithMany("Comments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Drive.Data.Entities.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Folder", "Folder")
                        .WithMany("Files")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Drive.Data.Entities.Models.User", "User")
                        .WithMany("Files")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.HasOne("Drive.Data.Entities.Models.Folder", "ParentFolder")
                        .WithMany("SubFolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Drive.Data.Entities.Models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentFolder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.File", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.Folder", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("SubFolders");
                });

            modelBuilder.Entity("Drive.Data.Entities.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}
