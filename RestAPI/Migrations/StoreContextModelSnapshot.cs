﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestAPI.Database;

#nullable disable

namespace RestAPI.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RestAPI.Models.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManagerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManagerFirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManagerLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("StoreCategory")
                        .HasColumnType("smallint");

                    b.Property<string>("StoreEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });
#pragma warning restore 612, 618
        }
    }
}