using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Models;
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
        public GlassesListPageViewModel(PageDialogService pageDialogService, INavigationService navigationService, GlassesManager glassesManager): base(pageDialogService, navigationService)
        {
            _glassesManager = glassesManager;
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
                    response = GetImageGlasses(response);
                    Glasses = new ObservableCollection<Glass>((response ?? new List<Glass>()).OrderBy(x => x.GlassName));
                    IsLoading = false;
                    
                }
                catch (Exception e)
                {
                    IsLoading = false;
                    await ShowMessage(ErrorMessages.ErrorOccured, e.Message, ErrorMessages.Ok);
                }
            }
        }
        private List<Glass> GetImageGlasses(List<Glass> glasses)
        {
            glasses?.ForEach(x => 
            {
                switch (x.GlassName)
                {
                    case "Margarita/Coupette glass":
                        x.GlassThumb = "margaritaCoupetteGlass.jpg";
                        break;
                    case "Nick and Nora Glass":
                        x.GlassThumb = "nickNoraGlass.jpg";
                        break;
                    case "Old-fashioned glass":
                        x.GlassThumb = "oldFGlass.jpg";
                        break;
                    default:
                        string image = new string(x.GlassName.ToLower()
                        .Select((y, z) => (z != 0 && x.GlassName[z - 1] == ' ') ? char.ToUpper(y) : y).ToArray()).Replace(" ", "");
                        x.GlassThumb = $"{image}.jpg";
                        break;
                }
            });
            return glasses.Where(x => !string.IsNullOrEmpty(x.GlassName)).ToList();
        }
            
    }
}
