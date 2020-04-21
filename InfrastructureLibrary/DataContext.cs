using CoreLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureLibrary
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Code> Code { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Activation> Activation { get; set; }
        public DbSet<CodeGroup> CodeGroup { get; set; }
        public DbSet<Device> Device { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<Group>().ToTable("Group");
            builder.Entity<Membership>().ToTable("Membership");
            builder.Entity<Code>().ToTable("Code");
            builder.Entity<Location>().ToTable("Location");
            builder.Entity<Activation>().ToTable("Activation");
            builder.Entity<CodeGroup>().ToTable("CodeGroup");
            builder.Entity<Device>().ToTable("Device").HasKey("MACAddress");

            base.OnModelCreating(builder);
        }
    }
}
