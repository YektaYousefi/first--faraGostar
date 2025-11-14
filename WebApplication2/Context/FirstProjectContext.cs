using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication2.Context
{
    public class FirstProjectContext : DbContext //IdentityDbContext<User>  
    {
        public FirstProjectContext(DbContextOptions<FirstProjectContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
