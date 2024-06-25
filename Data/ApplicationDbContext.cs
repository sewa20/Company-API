using Microsoft.EntityFrameworkCore;
using Practice1.CompanyInfo;

namespace Practice1.Data
{
    public class ApplicationDbContext : DbContext
    { 
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 

        }

        public DbSet<Info> Infos { get; set; }
    }
}
