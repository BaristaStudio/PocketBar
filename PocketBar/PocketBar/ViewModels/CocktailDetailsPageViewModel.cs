using Plugin.Share;
using Plugin.Share.Abstractions;
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
    class CocktailDetailsPageViewModel : BaseViewModel
    {
        private readonly ICocktailsManager _cocktailsManager;
        private readonly IIngredientsManager _ingredientsManager;
        const string FavoriteEmptyIcon = "favoritesUnfilled";
        const string FavoriteFilledIcon = "favoritesFilled";
        public string FavoriteIcon { get; set; } = FavoriteEmptyIcon;
        public Cocktail Cocktail {get; set; }
        public DelegateCommand ShareCocktailCommand { get; set; }
        public DelegateCommand MarkAsFavoriteCommand { get; set; }
        public DelegateCommand<string> GoToIngredientCommand { get; set; }
        public CocktailDetailsPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, ICocktailsManager cocktailsManager, IIngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
        {
            _cocktailsManager = cocktailsManager;
            _ingredientsManager = ingredientsManager;
            ShareCocktailCommand = new DelegateCommand(async () => { await ShareCocktail(); });
            MarkAsFavoriteCommand = new DelegateCommand(async() => { await ToggleFavorite(); });
            GoToIngredientCommand = new DelegateCommand<string>(async(param) => { await GoToIngredient(param); });            
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                if (!parameters.ContainsKey("DrinkId"))
                {
                    ShowMessage(Constants.ErrorMessages.ErrorOccured, Constants.ErrorMessages.MissingParameter, Constants.ErrorMessages.Ok);
                    return;
                }
                IsLoading = true;
                int drinkId = int.Parse(parameters["DrinkId"].ToString());
                if (parameters.ContainsKey("Cocktail"))
                {
                    Cocktail = parameters["Cocktail"] as Cocktail;
                }
                else
                {

                }
                FavoriteIcon = Cocktail.IsFavorite ? FavoriteFilledIcon : FavoriteEmptyIcon;
                IsLoading = false;
            } catch(Exception e)
            {
                IsLoading = false;
                ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async Task GetCocktail(int drinkId)
        {
            if(await HasInternetConnection(true))
            {
                Cocktail = await _cocktailsManager.GetCocktail(drinkId);
            }
        }
        async Task ToggleFavorite()
        {
            try
            {
                if (Cocktail.IsFavorite)
                {
                    _cocktailsManager.RemoveFromFavorites(Cocktail);
                }
                else
                {
                    _cocktailsManager.MarkAsFavorite(Cocktail);
                }
                FavoriteIcon = Cocktail.IsFavorite ? FavoriteFilledIcon : FavoriteEmptyIcon;
            } catch(Exception e)
            {
                await ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async Task GoToIngredient(string ingredientName)
        {
            try
            {
                Ingredient ingredient = null;
                if (await HasInternetConnection(true))
                    ingredient = await _ingredientsManager.GetIngredientByName(ingredientName);

                var parameters = new NavigationParameters
                {
                    { "ingredient", ingredient }
                };
                await NavigationService.NavigateAsync(new Uri(Constants.NavConstants.IngredientDetailsPage, UriKind.Relative), parameters);
            } catch(Exception e)
            {
                await ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async Task ShareCocktail()
        {
            try
            {
                var message = new ShareMessage()
                {
                    Title = "Pocket Bar",
                    Text = $"Check this awesome drink at Pocket Bar: {Cocktail.DrinkName} \n Instructions: {Cocktail.Instructions} \n For more information Go to Pocket Bar App"
                };
                await CrossShare.Current.Share(message);
            }
            catch(Exception e)
            {
                await ShowMessage(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
    }
}
