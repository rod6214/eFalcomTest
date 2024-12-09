﻿// <auto-generated />
using System;
using CentroDistribucion.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CentroDistribucion.Database.Migrations
{
    [DbContext(typeof(CentroDistribucionContext))]
    [Migration("20241208224115_Initial_Migration")]
    partial class Initial_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Movimiento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<long>("PalletId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PalletId");

                    b.ToTable("Movimientos", "dbo");
                });

            modelBuilder.Entity("Domain.Pallet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CodigoProducto")
                        .HasColumnType("bigint");

                    b.Property<long>("UbicacionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CodigoProducto");

                    b.HasIndex("UbicacionId");

                    b.ToTable("Pallets", "dbo");
                });

            modelBuilder.Entity("Domain.Ubicacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Columna")
                        .HasColumnType("int");

                    b.Property<int>("Fila")
                        .HasColumnType("int");

                    b.Property<bool>("Ocupado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Ubicaciones", "dbo");
                });

            modelBuilder.Entity("Domain.Movimiento", b =>
                {
                    b.HasOne("Domain.Pallet", "Pallet")
                        .WithMany("Movimientos")
                        .HasForeignKey("PalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pallet");
                });

            modelBuilder.Entity("Domain.Pallet", b =>
                {
                    b.HasOne("Domain.Ubicacion", "Ubicacion")
                        .WithMany("Pallets")
                        .HasForeignKey("UbicacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ubicacion");
                });

            modelBuilder.Entity("Domain.Pallet", b =>
                {
                    b.Navigation("Movimientos");
                });

            modelBuilder.Entity("Domain.Ubicacion", b =>
                {
                    b.Navigation("Pallets");
                });
#pragma warning restore 612, 618
        }
    }
}