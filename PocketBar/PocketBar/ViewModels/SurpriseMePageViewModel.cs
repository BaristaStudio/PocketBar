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
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand RefreshDataCommand { get; set; }

        public DelegateCommand ClearSearchCommand { get; set; }
        public bool HasData { get; set; }

        public string SearchTerm { get; set; }

        public bool IsRefreshing { get; set; } = false;

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
            this.SearchCommand = new DelegateCommand(SearchTermChanged);
            this.RefreshDataCommand = new DelegateCommand(GetData);
            this.GoToDrinkCommand = new DelegateCommand<string>(GoToDrink);
            this.GoToCocktailWithIngredientCommand = new DelegateCommand(GoToCocktailWithIngredient);
            this.GoToCocktailWithGlassCommand = new DelegateCommand(GoToCocktailWithGlass);
            this.ClearSearchCommand = new DelegateCommand(() => SearchTerm = string.Empty);
        }
        public async void GetData()
        {
            try
           {
                if (await HasInternetConnection(true))
                {
                    IsRefreshing = true;
                    RandomCocktail = await cocktailsManager.GetRandomCocktail();
                    RandomAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                    RandomNonAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
                    RandomIngredient = await ingredientsManager.GetRandomIngredient();
                    RandomGlass = await glassesManager.GetRandomGlass();
                }
                IsRefreshing = false;
            }
            catch (Exception e)
            {
                IsLoading = false;
                IsRefreshing = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async void GoToAlcoholicDrink()
        {
            try
            {
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomAlcoholicCocktail();
                    IsLoading = false;
                    this.GoToDrink(drink.IdDrink);
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
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomNonAlcoholicCocktail();
                    IsLoading = false;
                    this.GoToDrink(drink.IdDrink);
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
        public void SearchTermChanged()
        {
            typeAssistant.TextChanged();
        }
        public async void Search()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(SearchTerm) && await HasInternetConnection(true))
                {
                    IsFiltered = true;
                    FilteredCocktails = new ObservableCollection<Cocktail>(await cocktailsManager.FindCocktails(SearchTerm));
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
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.glassesManager.GetRandomCocktailByGlass(RandomGlass.GlassName);
                    IsLoading = false;
                    this.GoToDrink(drink.IdDrink);
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
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.ingredientsManager.GetRandomCocktailByIngredient(RandomIngredient.IngredientName);
                    IsLoading = false;
                    this.GoToDrink(drink.IdDrink);
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
