using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarmUpService.Models;
namespace WarmUpService.DataAccessLayer
{
    public class WarmUpContext : DbContext
    {
        public WarmUpContext(DbContextOptions<WarmUpContext> options)
              : base(options)
        {
        }

        
        public DbSet<Post> Posts { get; set; }
    }
}
