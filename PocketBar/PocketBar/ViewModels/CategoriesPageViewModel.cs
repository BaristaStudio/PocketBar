using PocketBar.Constants;
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
    class CategoriesPageViewModel: BaseViewModel
    {
        private readonly ICategoriesManager _categoriesManager;

        public IEnumerable<Category> Categories { get; set; }
        public DelegateCommand<string> GoToDrinksCommand { get; set; }
        public DelegateCommand GetCategoriesCommand { get; set; }
        public CategoriesPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, ICategoriesManager categoriesManager) : base(pageDialogService, navigationService)
        {
            _categoriesManager = categoriesManager;
            GoToDrinksCommand = new DelegateCommand<string>(async (param) => await GoToDrinks(param));
            GetCategoriesCommand = new DelegateCommand(async () => await GetCategories());
            GetCategoriesCommand.Execute();
        }
        public async Task GetCategories()
        {
            if (Categories == null && await HasInternetConnection(true))
            {
                try
                {
                    IsLoading = true;
                    var response = await _categoriesManager.GetCategories();
                    Categories = response?.OrderBy(x => x.CategoryName);
                    IsLoading = false;

                }
                catch (Exception e)
                {
                    IsLoading = false;
                    await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
                }
            }
        }
        public async Task GoToDrinks(string categoryName)
        {
            var parameters = new NavigationParameters
            {
                { "type", SearchType.Category },
                { "searchTerm", categoryName },
                { "title", $"Showing Cocktails By Category: {categoryName}" }
            };
            await NavigationService.NavigateAsync(new Uri(NavConstants.CocktailsListPage, UriKind.Relative), parameters);
        }
    }
}
