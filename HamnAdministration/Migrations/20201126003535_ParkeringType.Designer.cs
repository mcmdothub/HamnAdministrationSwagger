﻿// <auto-generated />
using System;
using HamnAdministration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HamnAdministration.Migrations
{
    [DbContext(typeof(HamnContext))]
    [Migration("20201126003535_ParkeringType")]
    partial class ParkeringType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HamnAdministration.Models.Bestall", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BestallStore");
                });

            modelBuilder.Entity("HamnAdministration.Models.Parkering", b =>
                {
                    b.Property<string>("Namn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsOpened")
                        .HasColumnType("bit");

                    b.HasKey("Namn");

                    b.ToTable("Parkering");
                });

            modelBuilder.Entity("HamnAdministration.Models.ParkeringsPlats", b =>
                {
                    b.Property<string>("ParkeringsNamn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Siffra")
                        .HasColumnType("int");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParkeringsNamn", "Siffra");

                    b.ToTable("ParkeringsPlatser");
                });

            modelBuilder.Entity("HamnAdministration.Models.ParkeringsPlats", b =>
                {
                    b.HasOne("HamnAdministration.Models.Parkering", "Parkering")
                        .WithMany("Platser")
                        .HasForeignKey("ParkeringsNamn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parkering");
                });

            modelBuilder.Entity("HamnAdministration.Models.Parkering", b =>
                {
                    b.Navigation("Platser");
                });
#pragma warning restore 612, 618
        }
    }
}
