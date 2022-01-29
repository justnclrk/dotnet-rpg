﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet_rpg.Data;

#nullable disable

namespace dotnet_rpg.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsId")
                        .HasColumnType("int");

                    b.HasKey("CharactersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("CharacterSkill");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Defeats")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("Fights")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Victories")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Damage = 20,
                            Name = "Punch"
                        },
                        new
                        {
                            Id = 2,
                            Damage = 30,
                            Name = "Kick"
                        },
                        new
                        {
                            Id = 3,
                            Damage = 50,
                            Name = "Throw"
                        });
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Player");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            PasswordHash = new byte[] { 130, 111, 61, 76, 255, 161, 144, 54, 120, 79, 251, 255, 198, 31, 117, 98, 33, 133, 228, 99, 15, 107, 15, 155, 138, 2, 137, 10, 147, 145, 65, 78, 137, 174, 150, 216, 18, 12, 187, 32, 150, 121, 169, 94, 112, 159, 74, 211, 205, 116, 72, 161, 88, 193, 195, 215, 76, 250, 194, 221, 33, 251, 42, 75 },
                            PasswordSalt = new byte[] { 160, 15, 219, 133, 73, 162, 229, 253, 81, 16, 236, 213, 184, 19, 79, 1, 15, 39, 218, 23, 54, 34, 12, 108, 228, 49, 145, 91, 125, 186, 111, 167, 213, 232, 234, 210, 237, 38, 47, 230, 114, 195, 167, 155, 65, 161, 225, 250, 227, 189, 104, 34, 159, 160, 17, 123, 191, 201, 216, 188, 23, 218, 99, 117, 117, 173, 68, 237, 230, 66, 63, 29, 56, 123, 144, 21, 224, 103, 70, 246, 245, 22, 236, 75, 205, 105, 142, 210, 9, 212, 88, 62, 30, 121, 185, 0, 139, 199, 13, 25, 38, 24, 170, 83, 12, 214, 72, 9, 97, 233, 120, 60, 60, 223, 60, 75, 95, 138, 178, 200, 53, 1, 159, 240, 184, 55, 31, 37 },
                            Username = "justin"
                        },
                        new
                        {
                            Id = 8,
                            PasswordHash = new byte[] { 130, 111, 61, 76, 255, 161, 144, 54, 120, 79, 251, 255, 198, 31, 117, 98, 33, 133, 228, 99, 15, 107, 15, 155, 138, 2, 137, 10, 147, 145, 65, 78, 137, 174, 150, 216, 18, 12, 187, 32, 150, 121, 169, 94, 112, 159, 74, 211, 205, 116, 72, 161, 88, 193, 195, 215, 76, 250, 194, 221, 33, 251, 42, 75 },
                            PasswordSalt = new byte[] { 160, 15, 219, 133, 73, 162, 229, 253, 81, 16, 236, 213, 184, 19, 79, 1, 15, 39, 218, 23, 54, 34, 12, 108, 228, 49, 145, 91, 125, 186, 111, 167, 213, 232, 234, 210, 237, 38, 47, 230, 114, 195, 167, 155, 65, 161, 225, 250, 227, 189, 104, 34, 159, 160, 17, 123, 191, 201, 216, 188, 23, 218, 99, 117, 117, 173, 68, 237, 230, 66, 63, 29, 56, 123, 144, 21, 224, 103, 70, 246, 245, 22, 236, 75, 205, 105, 142, 210, 9, 212, 88, 62, 30, 121, 185, 0, 139, 199, 13, 25, 38, 24, 170, 83, 12, 214, 72, 9, 97, 233, 120, 60, 60, 223, 60, 75, 95, 138, 178, 200, 53, 1, 159, 240, 184, 55, 31, 37 },
                            Username = "sam"
                        });
                });

            modelBuilder.Entity("dotnet_rpg.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.HasOne("dotnet_rpg.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnet_rpg.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dotnet_rpg.Models.Character", b =>
                {
                    b.HasOne("dotnet_rpg.Models.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Weapon", b =>
                {
                    b.HasOne("dotnet_rpg.Models.Character", "Character")
                        .WithOne("Weapon")
                        .HasForeignKey("dotnet_rpg.Models.Weapon", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Character", b =>
                {
                    b.Navigation("Weapon")
                        .IsRequired();
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
