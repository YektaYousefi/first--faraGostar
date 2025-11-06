using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Context
{
    public class FirstProjectContext: DbContext
    {
        public FirstProjectContext(DbContextOptions<FirstProjectContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }

    }
}
