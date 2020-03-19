using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.ViewModels
{
    class MyFavoritesPageViewModel: BaseViewModel
    {
        public MyFavoritesPageViewModel(PageDialogService pageDialogService, INavigationService navigationService) : base(pageDialogService, navigationService)
        {
        }
    }
}
