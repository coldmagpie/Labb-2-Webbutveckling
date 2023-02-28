using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class UserContext:DbContext
{
    public DbSet<UserModel> Users { get; set; }

    public UserContext(DbContextOptions options) : base(options)
    {

    }
}

