using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.ViewModels
{
    class GlassesListPageViewModel : BaseViewModel
    {
        public GlassesListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService): base(pageDialogService, navigationService)
            {
            }
            
    }
}
