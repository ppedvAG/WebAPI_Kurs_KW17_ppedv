#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Models;

namespace WebAPIKurs.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext (DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPIKurs.Models.ToDoItem> ToDoItem { get; set; }
    }
}
