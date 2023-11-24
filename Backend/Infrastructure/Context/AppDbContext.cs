using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }
        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<CompanyModel> Companies { get; set; }
    }
}
