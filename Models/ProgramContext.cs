using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Models
{
    public class ProgramContext : DbContext
    {
        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Children> Childrens { get; set; }
    }
}
