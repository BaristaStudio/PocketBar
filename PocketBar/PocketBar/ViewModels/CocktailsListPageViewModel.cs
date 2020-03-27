using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PocketBar.ViewModels
{
	class CocktailsListPageViewModel: BaseViewModel, IInitialize, INavigatedAware
	{
		private CategoriesManager categoriesManager;
		private GlassesManager glassesManager;
		private IngredientsManager ingredientsManager;
		private CocktailsManager cocktailsManager;
		public ObservableCollection<Cocktail> Cocktails { get; set; }
		public Cocktail _cocktailSelected { get; set; }
		public Cocktail CocktailSelected
		{
			get
			{
				return null;
			}
			set
			{
					_cocktailSelected = value;
					GoToDrink(_cocktailSelected.IdDrink);
			}
		}
		public string Title { get; set; }

		public DelegateCommand<Cocktail> ToggleFavoriteCommand { get; set; }


		public CocktailsListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService,CategoriesManager categoriesManager, GlassesManager glassesManager, IngredientsManager ingredientsManager, CocktailsManager cocktailsManager) : base(pageDialogService, navigationService)
        {

			this.categoriesManager = categoriesManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;
			this.cocktailsManager = cocktailsManager;

			ToggleFavoriteCommand = new DelegateCommand<Cocktail>(ToggleFavorite);
		}

		public async void GoToDrink(string drinkId)
		{
			try
			{
				var parameter = new NavigationParameters();
				parameter.Add("DrinkId", drinkId);
				await NavigationService.NavigateAsync(new System.Uri(NavConstants.CocktailDetailsPage, UriKind.Relative), parameter);

			}
			catch (Exception e)
			{
				await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
			}
		}

		public async void GetCocktailsByCategory(string Category)
		{
			if (await HasInternetConnection(true))
			{
				try
				{
					IsLoading = true;
					var response = await categoriesManager.GetCocktailsByCategory(Category);
					cocktailsManager.MapCocktailFavorites(response);
					Cocktails = response != null ? new ObservableCollection<Cocktail>(response.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

		public async void GetCocktailsByGlass(string Glass)
		{
			if (await HasInternetConnection(true))
			{
				try
				{
					IsLoading = true;
					var result = await glassesManager.GetCocktailsByGlass(Glass);
					cocktailsManager.MapCocktailFavorites(result);
					Cocktails = result != null ? new ObservableCollection<Cocktail>(result.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

		async void ToggleFavorite(Cocktail cocktail)
		{
			try
			{
				if (cocktail.IsFavorite)
				{
					cocktailsManager.RemoveFromFavorites(cocktail);
				}
				else
				{
					cocktailsManager.MarkAsFavorite(cocktail);
				}
			}
			catch (Exception e)
			{
				await ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
			}
		}

		public async void GetCocktailsByIngredient(string Ingredient)
		{
			if (await this.HasInternetConnection(true))
			{
				try
				{
					IsLoading = true;
					var result = await ingredientsManager.GetCocktailsByIngredient(Ingredient);
					cocktailsManager.MapCocktailFavorites(result);
					Cocktails = result != null ? new ObservableCollection<Cocktail>(result.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}

		public async void Initialize(INavigationParameters parameters)
		{
			try
			{
				SearchType? type;
				string searchTerm = "";
				if (parameters == null || parameters.Count == 0) return;
				if (parameters.ContainsKey("type"))
				{
					type = (SearchType)parameters["type"];
				}
				else
				{
					await ShowMessage(ErrorMessages.ErrorOccured, ErrorMessages.MissingInformation, ErrorMessages.Ok);
					return;
				}

				if (parameters.ContainsKey("searchTerm"))
				{
					searchTerm = parameters["searchTerm"] as string;
				}
				else
				{
					await ShowMessage(ErrorMessages.ErrorOccured, ErrorMessages.MissingInformation, ErrorMessages.Ok);
					return;
				}

				if (parameters.ContainsKey("title"))
				{
					Title = parameters.GetValue<string>("title");
				}
				else
				{
					await ShowMessage(ErrorMessages.ErrorOccured, ErrorMessages.MissingInformation, ErrorMessages.Ok);
					return;
				}

				if(type != null && !string.IsNullOrEmpty(searchTerm))
				{
					switch (type)
					{
						case SearchType.Category:
							GetCocktailsByCategory(searchTerm);
							break;
						case SearchType.Glass:
							GetCocktailsByGlass(searchTerm);
							break;
						case SearchType.Ingredient:
							GetCocktailsByIngredient(searchTerm);
							break;
					}
				}
			}catch(Exception e)
			{
				await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
			}
		}

		public void OnNavigatedFrom(INavigationParameters parameters)
		{
			// do nothing, we do not need this;
		}

		public void OnNavigatedTo(INavigationParameters parameters)
		{
			if(Cocktails != null && Cocktails.Count > 0)
			{
				IsLoading = true;
				cocktailsManager.MapCocktailFavorites(Cocktails.ToList());
				IsLoading = false;
			}
		}
	}
}
