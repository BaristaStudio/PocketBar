﻿using PocketBar.Models;
using PocketBar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Managers
{

	public class CategoriesManager
	{
		private CocktailService cocktailService;
		private List<Category> categories;

		public CategoriesManager(CocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Category>> GetCategories()
		{
			if (categories == null || categories.Count == 0)
			{
				var response = await cocktailService.ApiService.GetAllCategoriesAsync();
				categories = response.Categories.ToList();
			}
			return categories;
		}
		public async Task<List<Cocktail>> GetCocktailsByCategory(string category)
		{
			var response = await cocktailService.ApiService.GetCocktailsByCategoryAsync(category);
			return response.Drinks.ToList();
		}
	}
}
