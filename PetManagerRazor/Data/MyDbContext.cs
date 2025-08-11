using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Models;

namespace PetManagerRazor.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();

    }
}
