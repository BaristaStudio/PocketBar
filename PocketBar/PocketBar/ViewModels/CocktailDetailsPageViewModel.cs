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
            if (!parameters.ContainsKey("DrinkId"))
            {
                await ShowMessage("Error", Constants.ErrorMessages.ErrorOccured, "Ok");
                return;
            }
            int drinkId = int.Parse(parameters["DrinkId"].ToString());
            if (parameters.ContainsKey("Cocktail"))
            {
                Cocktail = parameters["Cocktail"] as Cocktail;
            }
            else
            {
                Cocktail = await _cocktailsManager.GetCocktail(drinkId);
            }
            IsFavorite = _cocktailsManager.IsFavorite(drinkId);
            FavoriteIcon = IsFavorite ? FavoriteFilledIcon : FavoriteEmptyIcon;
        }
        async void GoBack()
        {
            await NavigationService.GoBackAsync();
        }
        void MarkAsFavorite(string drinkId)
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
        }
        async void GoToIngredient(string ingredientName)
        {
            var ingredient = _ingredientsManager.GetIngredientByName(ingredientName);
            var parameters = new NavigationParameters
                {
                    { "ingredient", ingredient }
                };
            await NavigationService.NavigateAsync(new Uri(Constants.NavConstants.IngredientDetailsPage, UriKind.Relative), parameters);
        }
        void ShareCocktail()
        {
            var message = new ShareMessage()
            {
                Title = "Pocket Bar",
                Text = $"Check this awesome drink at Pocket Bar: {Cocktail.DrinkName}",
                Url = $"{Constants.NavConstants.AppURL}/{Constants.NavConstants.SurpriseMePage}/{Constants.NavConstants.CocktailDetailsPage}?DrinkId={Cocktail.IdDrink}"
            };
            CrossShare.Current.Share(message);
        }
    }
}
