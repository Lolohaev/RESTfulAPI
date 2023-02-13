namespace RestAPI.Models
{
	/// <summary>
	/// Stock model
	/// "характеристики магазина" из задания
	/// </summary>
	public class Stock
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Store Id
		/// </summary>
		public Guid StoreId { get; set; }

		/// <summary>
		/// Stock - Backstore
		/// </summary>
		public int BackStore { get; set; }

		/// <summary>
		/// Stock - Frontstore 
		/// </summary>
		public int Frontstore { get; set; }

		/// <summary>
		/// Stock - Shopping Window
		/// </summary>
		public int ShoppingWindow { get; set; }

		/// <summary>
		/// Stock Accuracy
		/// </summary>
		public double Accuracy { get; set; }

		/// <summary>
		/// On-Floor-Availability
		/// </summary>
		public double Availability { get; set; }

		///	<summary>
		/// Stock - Mean Age(days)
		/// </summary>
		public int MeanAge { get; set; }

	}
}
