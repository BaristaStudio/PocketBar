using PocketBar.Constants;
using PocketBar.ViewModels;
using PocketBar.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;

namespace PocketBar
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(NavConstants.PlaygroundPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IngredientListPage, IngredientListViewModel>();
            containerRegistry.RegisterForNavigation<GlassesListPage, GlassesListViewModel>();
            containerRegistry.RegisterForNavigation<CocktailsListPage, CocktailsListViewModel>();
            containerRegistry.RegisterForNavigation<CategoriesPage, CategoriesViewModel>();
            containerRegistry.RegisterForNavigation<CocktailDetailsPage, CocktailDetailsViewModel>();
            containerRegistry.RegisterForNavigation<IngredientDetailsPage, IngredientDetailsViewModel>();
            containerRegistry.RegisterForNavigation<SurpriseMePage, SurpriseMeViewModel>();
            containerRegistry.RegisterForNavigation<MyFavoritesPage, MyFavoritesViewModel>();

            containerRegistry.RegisterForNavigation<PlaygroundPage>();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
