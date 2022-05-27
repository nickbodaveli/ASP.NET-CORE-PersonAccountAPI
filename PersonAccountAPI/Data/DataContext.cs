using Microsoft.EntityFrameworkCore;
using PersonAccountAPI.Models;

namespace PersonAccountAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Bank> Banks { get; set; }

    }
}
