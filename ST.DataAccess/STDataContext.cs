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
            });
        }
    }
}
