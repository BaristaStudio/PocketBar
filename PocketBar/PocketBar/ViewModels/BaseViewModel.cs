using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PocketBar.ViewModels
{
    class BaseViewModel
    {
        INavigationService NavigationService { get; set; }
        IPageDialogService PageDialogService { get; set; }

        public Task<bool> HasInternetConnection()
        {
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task ShowMessage(string message)
        {
           return PageDialogService.DisplayAlertAsync("Error", "Please try again", "Ok");
        }
    }
}
