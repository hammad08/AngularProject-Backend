using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Phone> phones { get; set; }

        public DbSet<Address> addresses { get; set; }

        public DbSet<Customer> customers { get; set; }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured == false)
            {
                options.UseSqlServer("Server=.;Database=angularProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(c =>
            {
                c.Ignore(k => k.StateEnum);
                c.Property(p => p.firstName).IsRequired(true);
                c.Property(p => p.lastName).IsRequired(true);
                c.Property(p => p.email).IsRequired(true);
            });

            builder.Entity<Address>(c =>
            {
                c.Ignore(k => k.StateEnum);
                c.Property(k => k.street1).IsRequired(true);
                c.Property(k => k.zipcode).IsRequired(true);

            });

            builder.Entity<Phone>(c =>
            {
                c.Ignore(k => k.StateEnum);
            });
           
        }
    }
}
