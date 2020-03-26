using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PocketBar.ViewModels
{
    class GlassesListPageViewModel : BaseViewModel
    {
        private readonly GlassesManager _glassesManager;
        public ObservableCollection<Glass> Glasses { get; set; }
        public DelegateCommand<string> GoToDrinksCommand { get; set; }
        public GlassesListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, GlassesManager glassesManager): base(pageDialogService, navigationService)
        {
            _glassesManager = glassesManager;
            GoToDrinksCommand = new DelegateCommand<string>(GoToDrinks);
            GetGlasses();
        }
        public async void GetGlasses()
        {
            if(Glasses == null && await HasInternetConnection(true))
            {
                try
                {
                    IsLoading = true;
                    var response = await _glassesManager.GetGlasses();
                    Glasses = new ObservableCollection<Glass>((response ?? new List<Glass>()).Where(x => !string.IsNullOrEmpty(x.GlassName)).OrderBy(x => x.GlassName));
                    IsLoading = false;
                    
                }
                catch (Exception e)
                {
                    IsLoading = false;
                    await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
                }
            }
        }
        public async void GoToDrinks(string glassName)
        {
            var parameters = new NavigationParameters
            {
                { "type", SearchType.Glass },
                { "searchTerm", glassName },
                { "title", $"Showing Cocktails By Glass: {glassName}" }
            };
            await NavigationService.NavigateAsync(new Uri(NavConstants.CocktailsListPage, UriKind.Relative), parameters);
        }
            
    }
}
