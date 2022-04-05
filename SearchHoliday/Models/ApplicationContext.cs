using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HouseQuestion> HousesQuestions { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;uid=root;password=908056908;persistsecurityinfo=True;database=searchholiday;sslMode=none",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
