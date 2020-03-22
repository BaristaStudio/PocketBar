using PocketBar.Managers;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.ViewModels
{
    class CocktailDetailsPageViewModel : BaseViewModel, IInitialize
    {
        readonly CocktailsManager _cocktailsManager;
        public Cocktail Cocktail {get; set; }
        public DelegateCommand OnPressedBackCommand { get; set; }
        public DelegateCommand<string> GoToIngredientCommand { get; set; }
        public CocktailDetailsPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, CocktailsManager cocktailsManager) : base(pageDialogService, navigationService)
        {
            _cocktailsManager = cocktailsManager;
            OnPressedBackCommand = new DelegateCommand(async () =>
            {
                await NavigationService.GoBackAsync();
            });
            GoToIngredientCommand = new DelegateCommand<string>(async (ingredient) => 
            {
                await NavigationService.NavigateAsync(new Uri(Constants.NavConstants.IngredientDetailsPage, UriKind.Relative));
            });
        }
        public async void Initialize(INavigationParameters parameters)
        {
            if (!parameters.ContainsKey("DrinkId"))
            {
                await ShowMessage("Error", Constants.ErrorMessages.ErrorOccured, "Ok");
                return;
            }
            if (parameters.ContainsKey("Cocktail"))
            {
                Cocktail = parameters["Cocktail"] as Cocktail;
            }
            else
            {
                int drinkId = int.Parse(parameters["DrinkId"].ToString());
                Cocktail = await _cocktailsManager.GetCocktail(drinkId);
            }
        }
    }
}
