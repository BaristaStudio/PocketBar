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
	class CocktailsListPageViewModel: BaseViewModel, IInitialize
	{
		public ObservableCollection <Cocktail> Cocktails { get; set; }
		public Cocktail _cocktailSelected { get; set; }
		private CategoriesManager categoriesManager;
		private GlassesManager glassesManager;
		private IngredientsManager ingredientsManager;


		public string Title { get; set; }


		public CocktailsListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService,CategoriesManager categoriesManager, GlassesManager glassesManager, IngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
        {

			this.categoriesManager = categoriesManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;

		}

		public Cocktail CocktailSelected
		{
			get
			{
				return _cocktailSelected;
			}
			set
			{
				if (_cocktailSelected != value)
				{
					_cocktailSelected = value;
					GoToDrink(_cocktailSelected.IdDrink);
				}
			}
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
			if (await this.HasInternetConnection(true))
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

		public async void GetCocktailsByGlass(string Glass)
		{
			if (await this.HasInternetConnection(true))
			{
				try
				{
					IsLoading = true;
					var result = await glassesManager.GetCocktailsByGlass(Glass);
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



		public async void GetCocktailsByIngredient(string Ingredient)
		{
			if (await this.HasInternetConnection(true))
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
	}
}
