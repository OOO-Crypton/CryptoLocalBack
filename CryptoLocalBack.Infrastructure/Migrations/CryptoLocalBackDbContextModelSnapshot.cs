﻿// <auto-generated />
using System;
using CryptoLocalBack.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptoLocalBack.Infrastructure.Migrations
{
    [DbContext(typeof(CryptoLocalBackDbContext))]
    partial class CryptoLocalBackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptoLocalBack.Domain.Monitoring", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CurrentHashrate")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("EnergyConsumption")
                        .HasColumnType("double precision");

                    b.Property<double>("FanRPM")
                        .HasColumnType("double precision");

                    b.Property<double>("GPUTemperature")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("VideocardId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VideocardId");

                    b.ToTable("Monitorings");
                });

            modelBuilder.Entity("CryptoLocalBack.Domain.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("RigName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WalletCoinName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WalletName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("CryptoLocalBack.Domain.Videocard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CCDModel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CCDType")
                        .HasColumnType("integer");

                    b.Property<string>("CardManufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GPUFrequency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MemoryFrequency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Videocard");
                });

            modelBuilder.Entity("CryptoLocalBack.Domain.VideocardSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CoreLimit")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("MemoryLimit")
                        .HasColumnType("double precision");

                    b.Property<double>("PowerLimit")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("VideocardSettings");
                });

            modelBuilder.Entity("CryptoLocalBack.Domain.Monitoring", b =>
                {
                    b.HasOne("CryptoLocalBack.Domain.Videocard", "Videocard")
                        .WithMany("Monitorings")
                        .HasForeignKey("VideocardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Videocard");
                });

            modelBuilder.Entity("CryptoLocalBack.Domain.Videocard", b =>
                {
                    b.Navigation("Monitorings");
                });
#pragma warning restore 612, 618
        }
    }
}
