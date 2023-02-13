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
		public string StoreName { get; set; }

		/// <summary>
		/// Country Code
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// Store Email
		/// </summary>
		public string StoreEmail { get; set; }

		/// <summary>
		/// Store Mgr. First Name
		/// </summary>
		public string ManagerFirstName { get; set; }

		/// <summary>
		/// Store Mgr. Last Name
		/// </summary>
		public string ManagerLastName { get; set; }

		/// <summary>
		/// Store Manager Email
		/// </summary>
		public string ManagerEmail { get; set; }

		/// <summary>
		/// Category
		/// </summary>
		public byte StoreCategory { get; set; }
	}
}
