﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ST.DataAccess;

#nullable disable

namespace ST.DataAccess.Migrations
{
    [DbContext(typeof(STDataContext))]
    partial class STDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ST.Core.STColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CName")
                        .IsUnique();

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("ST.Core.STCustomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BillingAddress")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BillingAddress2")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BillingCity")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("BillingState")
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("BillingZip")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsBusinessCustomer")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessName")
                        .IsUnique()
                        .HasFilter("[BusinessName] IS NOT NULL");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ST.Core.STVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("VMake")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VModel")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VTrim")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VYear")
                        .HasColumnType("int(4)");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Vin")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("ST.Core.STVehicle", b =>
                {
                    b.HasOne("ST.Core.STColor", "Color")
                        .WithMany("Vehicles")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ST.Core.STCustomer", "Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ST.Core.STColor", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("ST.Core.STCustomer", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
