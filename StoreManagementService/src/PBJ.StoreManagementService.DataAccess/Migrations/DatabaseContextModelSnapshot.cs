﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PBJ.StoreManagementService.DataAccess.Context;

#nullable disable

namespace PBJ.StoreManagementService.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nchar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "CommentContent1",
                            CreatedAt = new DateTime(2023, 7, 19, 13, 24, 32, 943, DateTimeKind.Local).AddTicks(6535),
                            PostId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "CommentContent2",
                            CreatedAt = new DateTime(2023, 7, 19, 13, 24, 32, 943, DateTimeKind.Local).AddTicks(6584),
                            PostId = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "PostContent1",
                            CreatedAt = new DateTime(2023, 7, 19, 13, 24, 32, 944, DateTimeKind.Local).AddTicks(2440),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "PostContent2",
                            CreatedAt = new DateTime(2023, 7, 19, 13, 24, 32, 944, DateTimeKind.Local).AddTicks(2447),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2023, 7, 19, 13, 24, 32, 944, DateTimeKind.Local).AddTicks(5594),
                            Email = "login1",
                            LastName = "Lastname1",
                            Name = "Name1",
                            Surname = "Surname1"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2023, 7, 19, 13, 24, 32, 944, DateTimeKind.Local).AddTicks(5602),
                            Email = "login2",
                            LastName = "Lastname2",
                            Name = "Name2",
                            Surname = "Surname2"
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.UserFollowers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFollowers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FollowerId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            FollowerId = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Comment", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Post", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.UserFollowers", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Followings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
