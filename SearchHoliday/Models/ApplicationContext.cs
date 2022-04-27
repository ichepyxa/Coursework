using Microsoft.EntityFrameworkCore;
using System;

namespace SearchHoliday.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HouseQuestion> HousesQuestions { get; set; }
        public DbSet<HouseAnswer> HousesAnswers { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
