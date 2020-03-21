using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using PocketBar.Utils;
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
        public DelegateCommand GoToCocktailWithIngredientCommand { get; set; }
        public DelegateCommand GoToCocktailWithGlassCommand { get; set; }
        public DelegateCommand<string> GoToDrinkCommand { get; set; }
        public DelegateCommand<string> SearchCommand { get; set; }
        public DelegateCommand RefreshDataCommand { get; set; }
        public bool HasData { get; set; }

        private bool RefreshData = true;

        private TypeAssistant typeAssistant;
        public SurpriseMePageViewModel(PageDialogService pageDialogService, INavigationService navigationService, CocktailsManager cocktailsManager, IngredientsManager ingredientsManager, GlassesManager glassesManager) : base(pageDialogService, navigationService)
        {
            this.cocktailsManager = cocktailsManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;
            this.GetData();
            typeAssistant = new TypeAssistant(500);
            typeAssistant.OnFinishedTyping += Search;
            this.IsFiltered = false;
            this.GoToAlcoholicDrinkCommand = new DelegateCommand(GoToAlcoholicDrink);
            this.GoToNonAlcoholicDrinkCommand = new DelegateCommand(GoToNonAlcoholicDrink);
            this.SearchCommand = new DelegateCommand<string>(SearchTermChanged);
            this.RefreshDataCommand = new DelegateCommand(GetData);
            this.GoToDrinkCommand = new DelegateCommand<string>(GoToDrink);
            this.GoToCocktailWithIngredientCommand = new DelegateCommand(GoToCocktailWithIngredient);
            this.GoToCocktailWithGlassCommand = new DelegateCommand(GoToCocktailWithGlass);
        }
        public async void GetData()
        {
            try
            {
                if (!RefreshData)
                {
                    RefreshData = true;
                    return;
                }
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    RandomCocktail = await cocktailsManager.GetRandomCocktail();
                    RandomAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                    RandomNonAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                    RandomIngredient = await ingredientsManager.GetRandomIngredient();
                    RandomGlass = await glassesManager.GetRandomGlass();
                    IsLoading = false;
                }
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
                RefreshData = false;
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomAlcoholicCocktail();
                    this.GoToDrink(drink.IdDrink);
                    IsLoading = false;
                }
            }
            catch(Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async void GoToNonAlcoholicDrink()
        {
            try
            {
                RefreshData = false;
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomNonAlcoholicCocktail();
                    this.GoToDrink(drink.IdDrink);
                    IsLoading = false;
                }

            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async void GoToDrink(string drinkId)
        {
            try
            {
                var parameter = new NavigationParameters();
                parameter.Add("DrinkId", drinkId);
                if(drinkId == RandomCocktail.IdDrink)
                {
                    parameter.Add("Cocktail", RandomCocktail);
                }
                await NavigationService.NavigateAsync(new System.Uri(NavConstants.CocktailDetailsPage, UriKind.Relative), parameter);
            
            }
            catch (Exception e)
            {
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public void SearchTermChanged(string searchTerm)
        {
            typeAssistant.TextChanged(searchTerm);
        }
        public async void Search(string searchTerm)
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(searchTerm) && await HasInternetConnection(true))
                {
                    IsFiltered = true;
                    FilteredCocktails = new ObservableCollection<Cocktail>(await cocktailsManager.FindCocktails(searchTerm));
                    HasData = FilteredCocktails.Count > 0;
                }
                else
                {
                    IsFiltered = false;
                    HasData = false;
                }
                IsLoading = false;
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async void GoToCocktailWithGlass()
        {
            try
            {
                RefreshData = false;
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.glassesManager.GetRandomCocktailByGlass(RandomGlass.GlassName);
                    this.GoToDrink(drink.IdDrink);
                    IsLoading = false;
                }
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }

        }
        public async void GoToCocktailWithIngredient()
        {
            try
            {
                RefreshData = false;
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.ingredientsManager.GetRandomCocktailByIngredient(RandomIngredient.IngredientName);
                    this.GoToDrink(drink.IdDrink);
                    IsLoading = false;
                }
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }

        }
    }
}
