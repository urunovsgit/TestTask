﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.Database;

#nullable disable

namespace TestTask.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TestTask.Domain.DeliveryOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityRegion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DeliveryOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
