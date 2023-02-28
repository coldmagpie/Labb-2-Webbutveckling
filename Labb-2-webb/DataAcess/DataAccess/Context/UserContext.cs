using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class UserContext:DbContext
{
    public DbSet<UserModel> UserModels { get; set; }

    public UserContext(DbContextOptions options) : base(options)
    {

    }
}

