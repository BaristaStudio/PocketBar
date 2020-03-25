using PocketBar.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PocketBar.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigationService NavigationService { get; set; }
        public IPageDialogService PageDialogService { get; set; }
        public DelegateCommand OnPressedBackCommand { get; set; }
        public bool IsLoading { get; set; }

        public BaseViewModel(PageDialogService pageDialogService, INavigationService navigationService)
        {
            this.PageDialogService = pageDialogService;
            this.NavigationService = navigationService;
            OnPressedBackCommand = new DelegateCommand(GoBack);
        }

        public async Task<bool> HasInternetConnection(bool sendMessage = false)
        {
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                if (sendMessage)
                {
                    await App.Current.MainPage.DisplayAlert(Constants.ErrorMessages.NoInternet,Constants.ErrorMessages.NoInternetDescription, Constants.ErrorMessages.Ok);

                }
                return false;
            }
        }

        public Task ShowMessage(string title, string message, string cancel, string accept = null)
        {
           return PageDialogService.DisplayAlertAsync(title, message, accept, cancel);
        }

        public async void GoBack()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
