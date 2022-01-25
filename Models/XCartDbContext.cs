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

        public DbSet<Role> Role { get; set; }


    }
}
