﻿// <auto-generated />
using System;
using GalponIndustrial_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GalponIndustrial_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230924223020_NewTableNumeroGalpon")]
    partial class NewTableNumeroGalpon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GalponIndustrial_API.Models.Galpon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AlturaMax")
                        .HasColumnType("float");

                    b.Property<double>("AlturaMin")
                        .HasColumnType("float");

                    b.Property<int>("Baños")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Oficinas")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("m2Construidos")
                        .HasColumnType("int");

                    b.Property<int>("m2Totales")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Galpons");
                });

            modelBuilder.Entity("GalponIndustrial_API.Models.NumeroGalpon", b =>
                {
                    b.Property<int>("NroGalpon")
                        .HasColumnType("int");

                    b.Property<string>("Detalles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("GalponId")
                        .HasColumnType("int");

                    b.HasKey("NroGalpon");

                    b.HasIndex("GalponId");

                    b.ToTable("NumeroGalpones");
                });

            modelBuilder.Entity("GalponIndustrial_API.Models.NumeroGalpon", b =>
                {
                    b.HasOne("GalponIndustrial_API.Models.Galpon", "Galpon")
                        .WithMany()
                        .HasForeignKey("GalponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Galpon");
                });
#pragma warning restore 612, 618
        }
    }
}
