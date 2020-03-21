using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PocketBar.ViewModels
{
    class CocktailsListPageViewModel: BaseViewModel
	{
		public ObservableCollection <Cocktail> Cocktails { get; set; }
        private CategoriesManager categoriesManager;
		private GlassesManager glassesManager;
		private IngredientsManager ingredientsManager;


		public CocktailsListPageViewModel(CategoriesManager categoriesManager, GlassesManager glassesManager, IngredientsManager ingredientsManager) :base()
        {
            this.categoriesManager = categoriesManager;
			this.glassesManager = glassesManager;
			this.ingredientsManager = ingredientsManager;

			GetIngredient("Gin");
		}

		public async void GetCategories(string Category)
		{
			if (await this.HasInternetConnection())
			{
				try
				{
					IsLoading = true;
					var response = await categoriesManager.GetCocktailsByCategory(Category);
					Cocktails = response != null ? new ObservableCollection<Cocktail>(response.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

		public async void GetGlass(string Glass)
		{
			if (await this.HasInternetConnection())
			{
				try
				{
					IsLoading = true;
					var resutl = await glassesManager.GetCocktailsByGlass(Glass);
					Cocktails = resutl != null ? new ObservableCollection<Cocktail>(resutl.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

		public async void GetIngredient(string Ingredient)
		{
			if (await this.HasInternetConnection())
			{
				try
				{
					IsLoading = true;
					var resutl = await ingredientsManager.GetCocktailsByIngredient(Ingredient);
					Cocktails = resutl != null ? new ObservableCollection<Cocktail>(resutl.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

	}
}
