using PocketBar.Managers.Interfaces;
using PocketBar.Models;
using PocketBar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Managers
{

	public class CategoriesManager : ICategoriesManager
	{
		private ICocktailService cocktailService;
		private List<Category> categories;

		public CategoriesManager(ICocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Category>> GetCategories()
		{
			if (categories == null || categories.Count == 0)
			{
				try
				{
					var response = await cocktailService.ApiService.GetAllCategoriesAsync();
					categories = response.Categories.ToList();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			return categories;
		}
		public async Task<List<Cocktail>> GetCocktailsByCategory(string category)
		{
			try
			{
				var response = await cocktailService.ApiService.GetCocktailsByCategoryAsync(category);
				return response.Drinks.ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
