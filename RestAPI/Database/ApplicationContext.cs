using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Database
{
	/// <summary>
	/// настройки подключения к БД
	/// </summary>
	public class ApplicationContext : DbContext
	{
		public DbSet<Store> Stores { get; set; }
		public DbSet<Stock> Stocks { get; set; }

		public ApplicationContext(DbContextOptions options) : base(options)
		{
		}			
	}
}
