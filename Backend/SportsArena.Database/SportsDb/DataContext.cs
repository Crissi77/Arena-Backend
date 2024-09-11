
using Microsoft.EntityFrameworkCore;
using SportsArena.Models.DbModels;


namespace SportsArena.Database.SportsDb
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<UserDetails> userdetails { get;set;}
        public DbSet<UserAddress> useraddress { get;set;}
        public DbSet<Role> roles { get;set;}
        public DbSet<Product> product { get;set;}


    }
}

