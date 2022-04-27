#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ADWithMVC.Models;

namespace ADWithMVC.Data
{
    public class ADWithMVCContext : DbContext
    {
        public ADWithMVCContext (DbContextOptions<ADWithMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ADWithMVC.Models.Movie> Movie { get; set; }
    }
}
