using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Managers.Interfaces;
using PocketBar.Models;
using PocketBar.Utils;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.ViewModels
{
    class SurpriseMePageViewModel: BaseViewModel
    {
        private ICocktailsManager cocktailsManager;

        private IIngredientsManager ingredientsManager;

        private IGlassesManager glassesManager;

        private TypeAssistant typeAssistant;

        public Cocktail RandomCocktail { get; set; }

        public Ingredient RandomIngredient { get; set; }

        public Glass RandomGlass { get; set; }

        public bool IsFiltered { get; set; }

        public string SearchTerm { get; set; }

        public ObservableCollection<Cocktail> FilteredCocktails { get; set; }

        public DelegateCommand GoToAlcoholicDrinkCommand { get; set; }
        public DelegateCommand GoToNonAlcoholicDrinkCommand { get; set; }
        public DelegateCommand GoToCocktailWithIngredientCommand { get; set; }
        public DelegateCommand GoToCocktailWithGlassCommand { get; set; }
        public DelegateCommand<string> GoToDrinkCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand RefreshDataCommand { get; set; }
        public DelegateCommand ClearSearchCommand { get; set; }
        public SurpriseMePageViewModel(PageDialogService pageDialogService, INavigationService navigationService, ICocktailsManager cocktailsManager, IIngredientsManager ingredientsManager, IGlassesManager glassesManager) : base(pageDialogService, navigationService)
        {
            this.cocktailsManager = cocktailsManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;
            typeAssistant = new TypeAssistant(500);
            typeAssistant.OnFinishedTyping += OnSearch;
            IsFiltered = false;
            GoToAlcoholicDrinkCommand = new DelegateCommand(async() => {await GoToAlcoholicDrink();});
            GoToNonAlcoholicDrinkCommand = new DelegateCommand(async() => {await GoToNonAlcoholicDrink(); });
            SearchCommand = new DelegateCommand(SearchTermChanged);
            RefreshDataCommand = new DelegateCommand(async() => {await GetData(); });
            GoToDrinkCommand = new DelegateCommand<string>(async(param) => {await GoToDrink(param); });
            GoToCocktailWithIngredientCommand = new DelegateCommand(async() => {await GoToCocktailWithIngredient(); });
            GoToCocktailWithGlassCommand = new DelegateCommand(async() => {await GoToCocktailWithGlass(); });
            ClearSearchCommand = new DelegateCommand(() => SearchTerm = string.Empty);
            IsActiveChanged += new EventHandler(OnActivated);
            RefreshDataCommand.Execute();
        }
        private void OnActivated(object sender, EventArgs e)
        {
            if (IsActive && RandomCocktail!=null)
            {
                RandomCocktail.IsFavorite = cocktailsManager.IsFavorite(int.Parse(RandomCocktail.IdDrink));
            }
        }
        public async Task GetData()
        {
            try
           {
                if (await HasInternetConnection(true))
                {
                    var tasks = new[]
                    {
                        GetRandomCocktail(),
                        GetRandomIngredient(),
                        GetRandomGlass()
                    };
                    await Task.WhenAll(tasks);
                }
                IsRefreshing = false;
                IsLoading = false;
            }
            catch (Exception e)
            {
                IsLoading = false;
                IsRefreshing = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }

        public async Task GetRandomCocktail()
        {
            RandomCocktail = await cocktailsManager.GetRandomCocktail();
        }

        public async Task GetRandomIngredient()
        {
            RandomIngredient = await ingredientsManager.GetRandomIngredient();
        }

        public async Task GetRandomGlass()
        {
            RandomGlass = await glassesManager.GetRandomGlass();
        }

        public async Task GoToAlcoholicDrink()
        {
            try
            {
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomAlcoholicCocktail();
                    IsLoading = false;
                    await GoToDrink(drink.IdDrink);
                }
            }
            catch(Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }

        public async Task GoToNonAlcoholicDrink()
        {
            try
            {
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.cocktailsManager.GetRandomNonAlcoholicCocktail();
                    IsLoading = false;
                    await GoToDrink(drink.IdDrink);
                }

            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async Task GoToDrink(string drinkId)
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
        public async Task Search()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(SearchTerm) && await HasInternetConnection(true))
                {
                    IsFiltered = true;
                    FilteredCocktails = new ObservableCollection<Cocktail>(await cocktailsManager.FindCocktails(SearchTerm));
                    IsEmpty = FilteredCocktails.Count == 0;
                }
                else
                {
                    IsFiltered = false;
                    IsEmpty = false;
                }
                IsLoading = false;
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }

        public void OnSearch(object sender, EventArgs e) {
            Search();
        }
        public async Task GoToCocktailWithGlass()
        {
            try
            {
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.glassesManager.GetRandomCocktailByGlass(RandomGlass.GlassName);
                    IsLoading = false;
                    GoToDrink(drink.IdDrink);
                }
            }
            catch (Exception e)
            {
                IsLoading = false;
                await this.ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }

        }
        public async Task GoToCocktailWithIngredient()
        {
            try
            {
                if (await HasInternetConnection(true))
                {
                    IsLoading = true;
                    var drink = await this.ingredientsManager.GetRandomCocktailByIngredient(RandomIngredient.IngredientName);
                    IsLoading = false;
                    GoToDrink(drink.IdDrink);
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
