using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace RestAPI.Models
{
	/// <summary>
	/// Store model
	/// основные данные сайта" из задания
	/// </summary>
	public class Store
	{
		/// <summary>
		/// Guid
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Store Name
		/// </summary>
		[Required]
		public string? StoreName { get; set; }

		/// <summary>
		/// Country Code
		/// </summary>
		[Required] 
		public string? CountryCode { get; set; }

		/// <summary>
		/// Store Email
		/// </summary>
		[Required]
		public string? StoreEmail { get; set; }

		/// <summary>
		/// Store Mgr. First Name
		/// </summary>
		[Required]
		public string? ManagerFirstName { get; set; }

		/// <summary>
		/// Store Mgr. Last Name
		/// </summary>
		[Required]
		public string? ManagerLastName { get; set; }

		/// <summary>
		/// Store Manager Email
		/// </summary>
		[Required]
		public string? ManagerEmail { get; set; }

		/// <summary>
		/// Category
		/// </summary>
		public byte StoreCategory { get; set; }

		/// <summary>
		/// У каждого магазина имеется какая-то история изменения запасов
		/// </summary>
		public Stock? Stock { get; set; }
	}
}
