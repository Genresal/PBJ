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

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "CommentContent1",
                            CreatedAt = new DateTime(2023, 10, 9, 11, 31, 4, 473, DateTimeKind.Local).AddTicks(2543),
                            PostId = 2,
                            UserEmail = "unique1@email.com"
                        },
                        new
                        {
                            Id = 2,
                            Content = "CommentContent2",
                            CreatedAt = new DateTime(2023, 10, 9, 11, 31, 4, 473, DateTimeKind.Local).AddTicks(2596),
                            PostId = 1,
                            UserEmail = "unique2@email.com"
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserEmail");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "PostContent1",
                            CreatedAt = new DateTime(2023, 10, 9, 11, 31, 4, 473, DateTimeKind.Local).AddTicks(9576),
                            UserEmail = "unique1@email.com"
                        },
                        new
                        {
                            Id = 2,
                            Content = "PostContent2",
                            CreatedAt = new DateTime(2023, 10, 9, 11, 31, 4, 473, DateTimeKind.Local).AddTicks(9606),
                            UserEmail = "unique1@email.com"
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "unique1@email.com"
                        },
                        new
                        {
                            Id = 2,
                            Email = "unique2@email.com"
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.UserFollowers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FollowerEmail")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("FollowerEmail");

                    b.HasIndex("UserEmail");

                    b.ToTable("UserFollowers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FollowerEmail = "unique2@email.com",
                            UserEmail = "unique1@email.com"
                        },
                        new
                        {
                            Id = 2,
                            FollowerEmail = "unique1@email.com",
                            UserEmail = "unique2@email.com"
                        });
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Comment", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserEmail")
                        .HasPrincipalKey("Email")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.Post", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserEmail")
                        .HasPrincipalKey("Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PBJ.StoreManagementService.DataAccess.Entities.UserFollowers", b =>
                {
                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerEmail")
                        .HasPrincipalKey("Email")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PBJ.StoreManagementService.DataAccess.Entities.User", "User")
                        .WithMany("Followers")
                        .HasForeignKey("UserEmail")
                        .HasPrincipalKey("Email")
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
