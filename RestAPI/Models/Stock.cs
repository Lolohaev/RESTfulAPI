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
		/// Ссылка на магазин
		/// </summary>
		public Store? Store { get; set; }

		/// <summary>
		/// Stock - Backstore
		/// </summary>
		public int BackStore { get; set; } = 0;

		/// <summary>
		/// Stock - Frontstore 
		/// </summary>
		public int Frontstore { get; set; } = 0;

		/// <summary>
		/// Stock - Shopping Window
		/// </summary>
		public int ShoppingWindow { get; set; } = 0;

		/// <summary>
		/// Stock Accuracy
		/// </summary>
		public double Accuracy { get; set; } = 0.0;

		/// <summary>
		/// On-Floor-Availability
		/// </summary>
		public double Availability { get; set; } = 0.0;

		///	<summary>
		/// Stock - Mean Age(days)
		/// </summary>
		public int MeanAge { get; set; } = 0;

	}
}
