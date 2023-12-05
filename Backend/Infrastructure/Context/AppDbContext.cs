using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }
        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<StudentModel> Companies { get; set; }
    }
}
