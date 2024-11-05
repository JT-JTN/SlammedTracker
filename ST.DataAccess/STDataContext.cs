using Microsoft.EntityFrameworkCore;
using ST.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DataAccess
{
    public class STDataContext : DbContext
    {
        public STDataContext(DbContextOptions<STDataContext> options) : base(options)
        {
        }

        public DbSet<STCustomer> Customers { get; set; }
        public DbSet<STVehicle> Vehicles { get; set; }
        public DbSet<STColor> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<STCustomer>(c =>
            {
                c.HasKey(c => c.Id);
                c.HasIndex(c => c.EmailAddress).IsUnique();
                c.HasIndex(c => c.PhoneNumber).IsUnique();
                c.HasIndex(c => c.BusinessName).IsUnique();
                c.Property(c => c.FirstName).HasColumnType("nvarchar(50)");
                c.Property(c => c.LastName).HasColumnType("nvarchar(50)");
                c.Property(c => c.EmailAddress).HasColumnType("nvarchar(50)");
                c.Property(c => c.BusinessName).HasColumnType("nvarchar(50)");
                c.Property(c => c.BillingAddress).HasColumnType("nvarchar(100)");
                c.Property(c => c.BillingAddress2).HasColumnType("nvarchar(100)");
                c.Property(c => c.BillingCity).HasColumnType("nvarchar(30)");
                c.Property(c => c.BillingState).HasColumnType("nvarchar(2)");
                c.Property(c => c.BillingZip).HasColumnType("nvarchar(5)");
            });

            modelBuilder.Entity<STVehicle>(v =>
            {
                v.HasKey(v => v.Id);
                v.HasIndex(v => v.Vin).IsUnique();
                v.Property(v => v.Vin).HasColumnType("nvarchar(17)");
                v.Property(v => v.VYear).HasColumnType("int");
                v.Property(v => v.VMake).HasColumnType("nvarchar(50)");
                v.Property(v => v.VModel).HasColumnType("nvarchar(50)");
                v.Property(v => v.VTrim).HasColumnType("nvarchar(50)");
                v.HasOne(v => v.Customer).WithMany(c => c.Vehicles).HasForeignKey(v => v.CustomerId).OnDelete(DeleteBehavior.Restrict);
                v.HasOne(v => v.Color).WithMany(co => co.Vehicles).HasForeignKey(v => v.ColorId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<STColor>(co =>
            {
                co.HasKey(co => co.Id);
                co.HasIndex(co => co.CName).IsUnique();
                co.Property(co => co.CName).HasColumnType("nvarchar(50)");
            });
        }
    }
}
