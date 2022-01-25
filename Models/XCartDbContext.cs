using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.Models
{
    public class XCartDbContext : DbContext
    {
        protected XCartDbContext()
        {
        }
        public XCartDbContext(DbContextOptions<XCartDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<JobTitle> JobTitle { get; set; }
        public DbSet<Award> Award { get; set; }
        public DbSet<AwardHistory> AwardHistory { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<StatusDescription> StatusDescription { get; set; }


    }
}
