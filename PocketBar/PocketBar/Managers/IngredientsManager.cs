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
		private ICocktailAPIService _apiService;

		public IngredientsManager(ICocktailAPIService service)
		{
			this._apiService = service;
		}

		public async Task<List<Ingredient>> GetIngredients()
		{
			var response = await _apiService.GetAllIngredientsAsync();
			return response.Ingredients.ToList();
		}
	}
}
