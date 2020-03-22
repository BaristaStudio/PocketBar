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
				try
				{
					var response = await cocktailService.ApiService.GetAllIngredientsAsync();
					ingredients = response.Ingredients.ToList();
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			return ingredients;
		}

		public async Task<Ingredient> GetRandomIngredient()
		{
			try
			{
				if (ingredients == null || ingredients.Count == 0)
				{
					await GetIngredients();
				}
				Random rand = new Random();
				int randomPosition = rand.Next(0, this.ingredients.Count - 1);
				return ingredients[randomPosition];
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public async Task<Ingredient> GetIngredient(int id)
		{
			try
			{
				var ingredient = await cocktailService.ApiService.GetIngredientByIdAsync(id);
				return ingredient.Ingredients.FirstOrDefault();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public async Task<Ingredient> GetIngredientByName(string ingredientName)
		{
			try
			{
				var ingredient = await cocktailService.ApiService.SearchIngredientByNameAsync(ingredientName);
				return ingredient.Ingredients.FirstOrDefault();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public async Task<List<Cocktail>> GetCocktailsByIngredient(string ingredient)
		{
			try
			{
				var response = await cocktailService.ApiService.GetCocktailsByIngredientAsync(ingredient);
				return response.Drinks.ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public async Task<Cocktail> GetRandomCocktailByIngredient(string ingredient)
		{
			try
			{
				var cocktailsWithIngredient = await GetCocktailsByIngredient(ingredient);
				Random rand = new Random();
				int randomPosition = rand.Next(0, cocktailsWithIngredient.Count - 1);
				return cocktailsWithIngredient[randomPosition];
			}
			catch (Exception e)
			{
				throw e;
			}

		}
	}
}
