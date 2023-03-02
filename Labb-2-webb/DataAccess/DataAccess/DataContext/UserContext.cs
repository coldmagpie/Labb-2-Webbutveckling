using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccess.DataContext
{
    public class UserContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {

        }
    }
}
