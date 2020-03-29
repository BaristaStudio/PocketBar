using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Managers.Interfaces;
using PocketBar.Models;
using PocketBar.Services;
using Prism;
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
using System.Threading.Tasks;

namespace PocketBar.ViewModels
{
    class IngredientListPageViewModel: BaseViewModel
	{
		public ObservableCollection<Ingredient> Ingredients { get; set; }

		private IIngredientsManager ingredientsManager;
		public DelegateCommand<string> GoToIngredientCommand { get; set; }

		public IngredientListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService,IIngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
		{
			this.ingredientsManager = ingredientsManager;
			this.GoToIngredientCommand = new DelegateCommand<string>(async(param) => { await GoToIngredient(param); });
			GetIngredients();
		}
		public async Task GetIngredients()
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
		public async Task GoToIngredient(string ingredientName)
		{
			try
			{
				var parameter = new NavigationParameters();
				parameter.Add("ingredient", ingredientName);
				await NavigationService.NavigateAsync(new System.Uri(NavConstants.IngredientDetailsPage, UriKind.Relative), parameter);

			}
			catch (Exception e)
			{
				await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
			}
		}
	}
}
