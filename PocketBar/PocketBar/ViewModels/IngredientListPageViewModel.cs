using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using PocketBar.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PocketBar.ViewModels
{
    class IngredientListPageViewModel: BaseViewModel, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Ingredient> Ingredients { get; set; }
		public bool IsLoading { get; set; }

		private IngredientsManager ingredientsManager;

		public IngredientListPageViewModel() : base()
		{
			this.ingredientsManager = new IngredientsManager(this._apiService);
		}
		public async void GetIngredients()
		{
			if (await this.HasInternetConnection())
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
	}
}
