using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Database
{
	public class StoreContext : DbContext
	{
		public DbSet<Store> Stores { get; set; } = null!;

		public StoreContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();
		}

		/*
		 * попробовать разные варианты подключений
		/// <summary>
		/// строку подключения вынести в конфигурацию
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=Ruslan5555");
		}
		*/
	}
}
