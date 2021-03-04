﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrManager.DAL;

namespace PrManager.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210301211450_set_user_congregation_management")]
    partial class set_user_congregation_management
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PrManager.BL.Models.Congregation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CongregationName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CongregationNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("CongregationName", "CongregationNumber")
                        .IsUnique()
                        .HasFilter("[CongregationName] IS NOT NULL AND [CongregationNumber] IS NOT NULL");

                    b.ToTable("congregations");
                });

            modelBuilder.Entity("PrManager.BL.Models.Publicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CongregationId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("CongregationId");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("publicators");
                });

            modelBuilder.Entity("PrManager.BL.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("PrManager.BL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("PublicatorId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("PublicatorId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("PrManager.BL.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("RoleId", "UserId")
                        .IsUnique();

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("PrManager.BL.Models.Congregation", b =>
                {
                    b.HasOne("PrManager.BL.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("PrManager.BL.Models.Publicator", b =>
                {
                    b.HasOne("PrManager.BL.Models.Congregation", "Congregation")
                        .WithMany()
                        .HasForeignKey("CongregationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Congregation");
                });

            modelBuilder.Entity("PrManager.BL.Models.User", b =>
                {
                    b.HasOne("PrManager.BL.Models.Publicator", "Publicator")
                        .WithMany()
                        .HasForeignKey("PublicatorId");

                    b.Navigation("Publicator");
                });

            modelBuilder.Entity("PrManager.BL.Models.UserRole", b =>
                {
                    b.HasOne("PrManager.BL.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrManager.BL.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrManager.BL.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
