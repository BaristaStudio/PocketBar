using PocketBar.Models;
using PocketBar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Managers
{
	public class IngredientsManager
	{
		private CocktailService cocktailService;

		public IngredientsManager(CocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Ingredient>> GetIngredients()
		{
			var response = await cocktailService.ApiService.GetAllIngredientsAsync();
			return response.Ingredients.ToList();
		}
	}
}
