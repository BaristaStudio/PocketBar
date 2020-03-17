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
		private List<Ingredient> ingredients;

		public IngredientsManager(CocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Ingredient>> GetIngredients()
		{
			if(ingredients == null || ingredients.Count == 0)
			{
				var response = await cocktailService.ApiService.GetAllIngredientsAsync();
				ingredients = response.Ingredients.ToList();
			}
			return ingredients;
		}

		public async Task<Ingredient> GetRandomIngredient()
		{
			if(ingredients == null || ingredients.Count == 0)
			{
				await GetIngredients();
			}
			Random rand = new Random();
			int randomPosition = rand.Next(0, this.ingredients.Count - 1);
			return ingredients[randomPosition];
		}
		public async Task<Ingredient> GetIngredient(int id)
		{
			var ingredient = await cocktailService.ApiService.GetIngredientByIdAsync(id);
			return ingredient;
		}
		public async Task<List<Cocktail>> GetCocktailsByIngredient(string ingredient)
		{
			var response = await cocktailService.ApiService.GetCocktailsByIngredientAsync(ingredient);
			return response.Drinks.ToList();
		}
	}
}
