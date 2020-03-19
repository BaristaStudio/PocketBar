using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.ViewModels
{
    class CategoriesPageViewModel: BaseViewModel
    {
        public CategoriesPageViewModel(PageDialogService pageDialogService, INavigationService navigationService) : base(pageDialogService, navigationService)
        {
        }
    }
}
