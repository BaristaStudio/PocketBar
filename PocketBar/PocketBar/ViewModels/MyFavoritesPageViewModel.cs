using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Managers.Interfaces;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.ViewModels
{
    public class MyFavoritesPageViewModel: BaseViewModel
    {
        private ICocktailsManager cocktailsManager;
        public ObservableCollection<Cocktail> FavoriteCocktails { get; set; }
        public DelegateCommand<Cocktail> RemoveFromFavoritesCommand { get; set; }

        public DelegateCommand<string> GoToDrinkCommand { get; set; }
        public MyFavoritesPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, ICocktailsManager cocktailsManager) : base(pageDialogService, navigationService)
        {
            this.cocktailsManager = cocktailsManager;
            IsActiveChanged += new EventHandler(OnActivated);
            RemoveFromFavoritesCommand = new DelegateCommand<Cocktail>(async(param) => { await RemoveFromFavorites(param); });
            GoToDrinkCommand = new DelegateCommand<string>(async (param) => { await GoToDrink(param); });
        }

        private void OnActivated(object sender, EventArgs e)
        {
            if (IsActive)
            {
                GetFavorites();
            }
        }
        public async Task GetFavorites()
        {
            try
            {
                var response = cocktailsManager.GetFavorites();
                FavoriteCocktails = response != null ? new ObservableCollection<Cocktail>(response.OrderBy(i => i.DrinkName)) : new ObservableCollection<Cocktail>();
                IsEmpty = FavoriteCocktails.Count == 0;
            }
            catch (Exception e)
            {
                await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async Task RemoveFromFavorites(Cocktail cocktail)
        {
            try
            {
                cocktailsManager.RemoveFromFavorites(cocktail);
                FavoriteCocktails.Remove(FavoriteCocktails.FirstOrDefault(f => f.IdDrink == cocktail.IdDrink));
                IsEmpty = FavoriteCocktails.Count == 0;
            }
            catch (Exception e)
            {
                await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
        public async Task GoToDrink(string drinkId)
        {
            try
            {
                var parameter = new NavigationParameters();
                parameter.Add("DrinkId", drinkId);
                await NavigationService.NavigateAsync(new System.Uri(NavConstants.CocktailDetailsPage, UriKind.Relative), parameter);

            }
            catch (Exception e)
            {
                await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
            }
        }
    }
}
