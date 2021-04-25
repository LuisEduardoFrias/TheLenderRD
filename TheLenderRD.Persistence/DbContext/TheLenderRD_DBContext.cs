
using Microsoft.EntityFrameworkCore;
using TheLenderRD.Domain.Entitys;

namespace TheLenderRD.Persistence.DbContext
{
    public class TheLenderRD_DBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<AgeRate> AgeRates { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Month> Months { get; set; }

        public TheLenderRD_DBContext(Microsoft.EntityFrameworkCore.DbContextOptions<TheLenderRD_DBContext> option) : base(option)
        {

        }
    }
}
