using PocketBar.Models;
using PocketBar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Managers
{
	public class GlassesManager
	{
		private CocktailService cocktailService;
		private List<Glass> glasses;

		public GlassesManager(CocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Glass>> GetGlasses()
		{
			if (glasses == null || glasses.Count == 0)
			{
				try
				{
					var response = await cocktailService.ApiService.GetAllGlassesAsync();
					glasses = response.Glasses.ToList();
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			return glasses;
		}
		public async Task<Glass> GetRandomGlass()
		{
			if (glasses == null || glasses.Count == 0)
			{
				try
				{
					await GetGlasses();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			Random rand = new Random();
			int randomPosition = rand.Next(0, glasses.Count - 1);
			return glasses[randomPosition];
		}
		public async Task<List<Cocktail>> GetCocktailsByGlass(string glass)
		{
			try
			{
				var response = await cocktailService.ApiService.GetCocktailsByGlassAsync(glass);
				return response.Drinks.ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public async Task<Cocktail> GetRandomCocktailByGlass(string glass)
		{
			try
			{
				var cocktailsWithGlass = await GetCocktailsByGlass(glass);
				Random rand = new Random();
				int randomPosition = rand.Next(0, cocktailsWithGlass.Count - 1);
				return cocktailsWithGlass[randomPosition];
			}
			catch (Exception e)
			{
				throw e;
			}

		}

	}
}
