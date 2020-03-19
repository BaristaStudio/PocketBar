using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PocketBar.ViewModels
{
    class SurpriseMePageViewModel: BaseViewModel
    {
        private CocktailsManager cocktailsManager;

        private IngredientsManager ingredientsManager;

        private GlassesManager glassesManager;
        public Cocktail RandomCocktail { get; set; }
        public Cocktail RandomAlcoholicCocktail { get; set; }
        public Cocktail RandomNonAlcoholicCocktail { get; set; }
        public Ingredient RandomIngredient { get; set; }
        public Glass RandomGlass { get; set; }
        public bool IsFiltered { get; set; }
        public ObservableCollection<Cocktail> FilteredCocktails { get; set; }
        public DelegateCommand GoToAlcoholicDrinkCommand { get; set; }
        public DelegateCommand GoToNonAlcoholicDrinkCommand { get; set; }
        public DelegateCommand<string> SearchCommand { get; set; }
        public bool HasData { get; set; }
        public SurpriseMePageViewModel(PageDialogService pageDialogService, INavigationService navigationService, CocktailsManager cocktailsManager, IngredientsManager ingredientsManager, GlassesManager glassesManager) : base(pageDialogService, navigationService)
        {
            this.cocktailsManager = cocktailsManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;
            this.IsFiltered = false;
            this.GoToAlcoholicDrinkCommand = new DelegateCommand(GoToAlcoholicDrink);
            this.GoToNonAlcoholicDrinkCommand = new DelegateCommand(GoToNonAlcoholicDrink);
            this.SearchCommand = new DelegateCommand<string>(Search);
        }

        public async void GetData()
        {
            try
            {
                IsLoading = true;
                RandomCocktail = await cocktailsManager.GetRandomCocktail();
                RandomAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                RandomNonAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                RandomIngredient = await ingredientsManager.GetRandomIngredient();
                RandomGlass = await glassesManager.GetRandomGlass();
                IsLoading = false;
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }

        public async void GoToAlcoholicDrink()
        {
            try
            {
                IsLoading = true;
                var drink = await this.cocktailsManager.GetRandomAlcoholicCocktail();
                this.GoToDrink(int.Parse(drink.IdDrink));
                IsLoading = false;

            }
            catch(Exception e)
            {
                IsLoading = false;
            }
        }
        public async void GoToNonAlcoholicDrink()
        {
            try
            {
                IsLoading = true;
                var drink = await this.cocktailsManager.GetRandomNonAlcoholicCocktail();
                this.GoToDrink(int.Parse(drink.IdDrink));
                IsLoading = false;

            }
            catch (Exception e)
            {
                IsLoading = false;
            }
        }
        public async void GoToDrink(int drinkId)
        {
            try
            {
                var parameter = new NavigationParameters();
                parameter.Add("DrinkId", drinkId);
                await NavigationService.NavigateAsync(new Uri(NavConstants.CocktailDetailsPage, UriKind.Relative), parameter);
            
            }
            catch (Exception e)
            {
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }

        public async void Search(string searchTerm)
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    IsFiltered = true;
                    FilteredCocktails = new ObservableCollection<Cocktail>(await cocktailsManager.FindCocktails(searchTerm));
                }
                else
                {
                    IsFiltered = false;
                }
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
