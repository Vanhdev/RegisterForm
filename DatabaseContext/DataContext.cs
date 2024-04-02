﻿using Microsoft.EntityFrameworkCore;
using RegisterForm.Models;

namespace RegisterForm.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
