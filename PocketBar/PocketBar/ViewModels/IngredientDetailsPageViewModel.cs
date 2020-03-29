using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Managers.Interfaces;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.ViewModels
{
    class IngredientDetailsPageViewModel: BaseViewModel
    {
        private  ICocktailsManager _cocktailsManager;
        private  IIngredientsManager _ingredientsManager;
        public DelegateCommand<string> GoToDrinksCommand { get; set; }
        public Ingredient Ingredient { get; set; }
        public IngredientDetailsPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, ICocktailsManager cocktailsManager, IIngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
        {
            _cocktailsManager = cocktailsManager;
            _ingredientsManager = ingredientsManager;
            GoToDrinksCommand = new DelegateCommand<string>(async(param) => {await GoToDrinksByIngredient(param); });

        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                if (!parameters.ContainsKey("ingredient"))
                {
                    await ShowMessage(Constants.ErrorMessages.ErrorOccured, Constants.ErrorMessages.MissingParameter, Constants.ErrorMessages.Ok);
                    return;
                }
                IsLoading = true;
                string ingredientName = parameters["ingredient"].ToString();
                
                if (await HasInternetConnection(true))
                {
                    Ingredient = await _ingredientsManager.GetIngredientByName(ingredientName);
                }
                IsLoading = false;

            }
            catch (Exception e)
            {
                IsLoading = false;
                await ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }

        public async Task GoToDrinksByIngredient(string ingredientName)
        {
            try
            {
                var parameter = new NavigationParameters();
                parameter.Add("searchTerm", ingredientName);
                parameter.Add("type", SearchType.Ingredient);
                parameter.Add("title", String.Format("Showing Cocktails By Ingredient: {0}", ingredientName)); 
                await NavigationService.NavigateAsync(new Uri(NavConstants.CocktailsListPage, UriKind.Relative), parameter);
            }
            catch(Exception e)
            {
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
    }
}
