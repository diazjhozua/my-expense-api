using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using my_expense_api.Models;

namespace my_expense_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}