using Plugin.Share;
using Plugin.Share.Abstractions;
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
        private readonly CocktailsManager _cocktailsManager;
        private readonly IngredientsManager _ingredientsManager;
        const string FavoriteEmptyIcon = "favorites";
        const string FavoriteFilledIcon = "favoritesFilled";
        public string FavoriteIcon { get; set; } = FavoriteEmptyIcon;
        public bool IsFavorite { get; set; } = false;
        public Cocktail Cocktail {get; set; }
        public DelegateCommand OnPressedBackCommand { get; set; }
        public DelegateCommand ShareCocktailCommand { get; set; }
        public DelegateCommand<string> MarkAsFavoriteCommand { get; set; }
        public DelegateCommand<string> GoToIngredientCommand { get; set; }
        public CocktailDetailsPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, CocktailsManager cocktailsManager, IngredientsManager ingredientsManager) : base(pageDialogService, navigationService)
        {
            _cocktailsManager = cocktailsManager;
            _ingredientsManager = ingredientsManager;
            OnPressedBackCommand = new DelegateCommand(GoBack);
            ShareCocktailCommand = new DelegateCommand(ShareCocktail);
            MarkAsFavoriteCommand = new DelegateCommand<string>(MarkAsFavorite);
            GoToIngredientCommand = new DelegateCommand<string>(GoToIngredient);            
        }
        public async void Initialize(INavigationParameters parameters)
        {
            try
            {
                if (!parameters.ContainsKey("DrinkId"))
                {
                    await ShowMessage(Constants.ErrorMessages.ErrorOccured, Constants.ErrorMessages.MissingParameter, Constants.ErrorMessages.Ok);
                    return;
                }
                IsLoading = true;
                int drinkId = int.Parse(parameters["DrinkId"].ToString());
                if (parameters.ContainsKey("Cocktail"))
                {
                    Cocktail = parameters["Cocktail"] as Cocktail;
                }
                else if(await HasInternetConnection(true))
                {
                    Cocktail = await _cocktailsManager.GetCocktail(drinkId);
                }
                IsFavorite = _cocktailsManager.IsFavorite(drinkId);
                FavoriteIcon = IsFavorite ? FavoriteFilledIcon : FavoriteEmptyIcon;
                IsLoading = false;
            } catch(Exception e)
            {
                IsLoading = false;
                await PageDialogService.DisplayAlertAsync(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async void GoBack()
        {
            await NavigationService.GoBackAsync();
        }
        async void MarkAsFavorite(string drinkId)
        {
            try
            {
                if (IsFavorite)
                {
                    _cocktailsManager.RemoveFromFavorites(int.Parse(drinkId));
                }
                else
                {
                    _cocktailsManager.MarkAsFavorite(Cocktail);
                }
                IsFavorite = !IsFavorite;
                FavoriteIcon = IsFavorite ? FavoriteFilledIcon : FavoriteEmptyIcon;
            } catch(Exception e)
            {
                await PageDialogService.DisplayAlertAsync(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async void GoToIngredient(string ingredientName)
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
                await PageDialogService.DisplayAlertAsync(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
        async void ShareCocktail()
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
                await PageDialogService.DisplayAlertAsync(Constants.ErrorMessages.ErrorOccured, e.Message, Constants.ErrorMessages.Ok);
            }
        }
    }
}
