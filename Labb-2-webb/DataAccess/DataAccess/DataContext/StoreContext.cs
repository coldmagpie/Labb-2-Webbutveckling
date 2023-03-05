using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccess.DataContext
{
    public class StoreContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        public StoreContext(DbContextOptions options) : base(options)
        {

        }
    }
}
