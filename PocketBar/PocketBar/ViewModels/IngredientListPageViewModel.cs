using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using PocketBar.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PocketBar.ViewModels
{
    class IngredientListPageViewModel: BaseViewModel
	{
		public ObservableCollection<Ingredient> Ingredients { get; set; }

		private IngredientsManager ingredientsManager;
		public DelegateCommand<string> GoToIngredientCommand { get; set; }

		public IngredientListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService,IngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
		{
			this.ingredientsManager = ingredientsManager;
			this.GoToIngredientCommand = new DelegateCommand<string>(GoToIngredient);
			GetIngredients();
		}
		public async void GetIngredients()
		{
			if (Ingredients == null && await HasInternetConnection(true))
			{
				try
				{
					IsLoading = true;
					var response = await ingredientsManager.GetIngredients();

					Ingredients = response != null ? new ObservableCollection<Ingredient>(response.OrderBy(i => i.IngredientName)) : new ObservableCollection<Ingredient>();
					IsLoading = false;
				}
				catch (Exception e)
				{
					IsLoading = false;
					await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
				}
			}

		}
		public async void GoToIngredient(string ingredientName)
		{
			try
			{
				var parameter = new NavigationParameters();
				parameter.Add("Ingredient", ingredientName);
				await NavigationService.NavigateAsync(new System.Uri(NavConstants.IngredientDetailsPage, UriKind.Relative), parameter);

			}
			catch (Exception e)
			{
				await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
			}
		}
	}
}
