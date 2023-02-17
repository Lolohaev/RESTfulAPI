using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Database;
using RestAPI.Models;

namespace RestAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{
		public readonly ApplicationContext _dbContext;

		public StoreController(ApplicationContext applicationContext)
		{
			_dbContext = applicationContext;
		}

		/// <summary>
		/// обзор всех магазинов
		/// </summary>
		/// <returns>возвращаем магазины</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Store>>> GetStores()
		{
			if (_dbContext.Stores == null)
			{
				return NotFound();
			}
			return await _dbContext.Stores.ToListAsync();
		}

		/// <summary>
		/// Возможно неверно понял что требуется
		/// просто отдельно вычислил все минимальные-максимальные-средние значения по всем магазинам
		/// и вынес их в ответ. Так же в ответе есть данные по текущему магазину. 
		/// </summary>
		/// <param name="id">Идентификатор магазина</param>
		/// <returns>метрики агрегации по магазинам</returns>
		[HttpGet("/metricks/{id}")]
		public async Task<ActionResult<AggregationMetricks>> GetStoresAggregationMetricks(Guid id)
		{
			if (_dbContext.Stocks == null)
			{
				return NotFound();
			}
			var stock = await _dbContext.Stocks.FindAsync(id);

			if (stock == null)
			{
				return NotFound();
			}

			var metricks = GetAggregation();
			metricks.currentMeanAge = stock.MeanAge;
			metricks.currentAccuracy = stock.Accuracy;
			metricks.currentAvailability= stock.Availability;
			metricks.currentValue = stock.Frontstore + stock.BackStore + stock.ShoppingWindow;

			return metricks;
		}

		/// <summary>
		/// просмотр конкретного магазина
		/// </summary>
		/// <param name="id">Идентификатор магазина</param>
		/// <returns>данные магазина</returns>
		[HttpGet("/store/{id}")]
		public async Task<ActionResult<Store>> GetStore(Guid id)
		{
			if (_dbContext.Stores == null)
			{
				return NotFound();
			}
			var store = await _dbContext.Stores.FindAsync(id);

			if (store == null)
			{
				return NotFound();
			}

			return store;
		}

		/// <summary>
		/// просмотр остатков магазина (Просмотр данных "Характеристики хранилища" (хранение по хранилищам))
		/// </summary>
		/// <param name="id">Идентификатор магазина</param>
		/// <returns>данные магазина</returns>
		[HttpGet("/stock/{id}")]
		public async Task<ActionResult<Stock>> GetStock(Guid Id)
		{
			if (_dbContext.Stocks == null)
			{
				return NotFound();
			}
			var stock = await _dbContext.Stocks.FindAsync(Id);

			if (stock == null)
			{
				return NotFound();
			}

			return stock;
		}

		/// <summary>
		/// Добавляем магазин. 
		/// К каждому магазину идет его строка с данными о количестве материала.
		/// Так как мы создаем только магазин, то и количество данных будет равно нулю. 
		/// </summary>
		/// <param name="store">данные магазина</param>
		/// <returns> созданный магазин </returns>
		[HttpPost]
		public async Task<ActionResult<Store>> AddStore(Store store)
		{
			_dbContext.Stores.Add(store);
			await _dbContext.SaveChangesAsync();
			
			if (store.Stock == null)
			{
				_dbContext.Stocks.Add(new Stock { StoreId = store.Id, Store = store });
			}
			else
			{
				_dbContext.Stocks.Add(store.Stock);
			}
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetStores), new { name = store.StoreName }, store);
		}

		/// <summary>
		/// меняем данные магазина
		/// </summary>
		/// <param name="id">айди магазина</param>
		/// <param name="store">новые данные магазина</param>
		/// <returns>ок если все хорошо, ошибки если что-то не так</returns>
		[HttpPut("/store/{id}")]
		public async Task<IActionResult> ChangeStoreData(Guid id, Store store)
		{
			if (id != store.Id)
			{
				return BadRequest();
			}

			_dbContext.Entry(store).State = EntityState.Modified;

			try
			{
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_dbContext.Stores?.Any(s => s.Id == id)).GetValueOrDefault())
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok();
		}

		/// <summary>
		/// меняем данные по магазину
		/// </summary>
		/// <param name="id">айди данных</param>
		/// <param name="stock">новые данные</param>
		/// <returns></returns>
		[HttpPut("/stock/{id}")]
		public async Task<IActionResult> ChangeStockData(Guid id, Stock stock)
		{
			/*
			В данном месте возник вопрос - а не надо ли нам добавить условие, что в магазине может быть только положительное количество товара?
			Но так как мы живем в системе, где помимо дебета существует кредит решил не мудрить.
			 */
			if (id != stock.Id)
			{
				return BadRequest();
			}

			_dbContext.Entry(stock).State = EntityState.Modified;

			try
			{
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_dbContext.Stocks?.Any(s => s.Id == id)).GetValueOrDefault())
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok();
		}

		/// <summary>
		/// удаляем данные магазина. Данные по остаткам удаляются каскадно
		/// </summary>
		/// <param name="id">айди магазина</param>
		/// <returns>ок если все хорошо, NF если такого магазина нет</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStore(Guid id)
		{
			if (_dbContext.Stores == null)
			{
				return NotFound();
			}

			var store = await _dbContext.Stores.FindAsync(id);
			if (store == null)
			{
				return NotFound();
			}

			_dbContext.Stores.Remove(store);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		private AggregationMetricks GetAggregation()
		{
			var stocks = _dbContext.Stocks.ToList();
			var totalValue = stocks.Select(i => new { Value = i.BackStore + i.Frontstore + i.ShoppingWindow, Accuracy = i.Accuracy, MeanAge = i.MeanAge, Availability = i.Availability }).ToList();

			return new AggregationMetricks()
			{
				minValue = totalValue.Select(i => i.Value).Min(),
				maxValue = totalValue.Select(i => i.Value).Max(),
				averageValue = totalValue.Select(i => i.Value).Average(),

				minAccuracy = totalValue.Select(i => i.Accuracy).Min(),
				maxAccuracy = totalValue.Select(i => i.Accuracy).Max(),
				averageAccuracy = totalValue.Select(i => i.Accuracy).Average(),

				minMeanAge = totalValue.Select(i => i.MeanAge).Min(),
				maxMeanAge = totalValue.Select(i => i.MeanAge).Max(),
				averageMeanAge = totalValue.Select(i => i.MeanAge).Average(),

				minAvailability = totalValue.Select(i => i.Availability).Min(),
				maxAvailability = totalValue.Select(i => i.Availability).Max(),
				averageAvailability = totalValue.Select(i => i.Availability).Average()
			};
		}

		public class AggregationMetricks
		{
			public int minValue { get; set; }
			public int maxValue { get; set; }
			public double averageValue { get; set; }
			public int? currentValue { get; set; }

			public double minAccuracy { get; set; }
			public double maxAccuracy { get; set; }
			public double averageAccuracy { get; set; }
			public double? currentAccuracy { get; set; }

			public int minMeanAge { get; set; }
			public int maxMeanAge { get; set; }
			public double averageMeanAge { get; set; }
			public int? currentMeanAge { get; set; }

			public double minAvailability { get; set; }
			public double maxAvailability { get; set; }
			public double averageAvailability { get; set; }
			public double? currentAvailability { get; set; }
		}
	}
}
