global using Microsoft.EntityFrameworkCore;
using System;
using BacktestAPI.Models;

namespace BacktestAPI.Data
{
	public class DataContext:DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // use of POSTGRESS install packages
            // Npqsql EntityFramework POstgreSQL
            // Npqsql EntityFramework POstgreSQLDesign
            // make sure u have posgress command line tools installed locally and u can run 
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=backtest;Username=mikolajgruszecki;Password=Organy9016;");
            // Port=5432 is default port,  
        }

        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
    }
}


