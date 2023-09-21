using Microsoft.EntityFrameworkCore;

namespace ATMSimulation.API;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options){}
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //insert static data
        modelBuilder.Entity<User>().HasData(

              new User { CardNumber = "12345678955555", Name = "Mahmoud Elish", PIN = "123456", Balance = 8000 },
              new User { CardNumber = "56789012345678", Name = "Ali", PIN = "666666", Balance = 2500 },
              new User { CardNumber = "12345678901234", Name = "Radwa", PIN = "000000", Balance = 9100 }
        );
    }
}
