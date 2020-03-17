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
				var response = await cocktailService.ApiService.GetAllGlassesAsync();
				glasses = response.Glasses.ToList();
			}
			return glasses;
		}

		public async Task<Glass> GetRandomGlass()
		{
			if (glasses == null || glasses.Count == 0)
			{
				await GetGlasses();
			}
			Random rand = new Random();
			int randomPosition = rand.Next(0, glasses.Count - 1);
			return glasses[randomPosition];
		}
		public async Task<List<Cocktail>> GetCocktailsByGlass(string glass)
		{
			var response = await cocktailService.ApiService.GetCocktailsByGlassAsync(glass);
			return response.Drinks.ToList();
		}

	}
}
