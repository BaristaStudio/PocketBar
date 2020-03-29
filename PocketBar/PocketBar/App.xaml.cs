using PocketBar.Constants;
using PocketBar.Managers;
using PocketBar.Managers.Interfaces;
using PocketBar.Services;
using PocketBar.ViewModels;
using PocketBar.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Refit;
using System;

namespace PocketBar
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
           NavigationService.NavigateAsync(NavConstants.MainTabbedPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<IngredientListPage, IngredientListPageViewModel>();
            containerRegistry.RegisterForNavigation<GlassesListPage, GlassesListPageViewModel>();
            containerRegistry.RegisterForNavigation<CocktailsListPage, CocktailsListPageViewModel>();
            containerRegistry.RegisterForNavigation<CategoriesPage, CategoriesPageViewModel>();
            containerRegistry.RegisterForNavigation<CocktailDetailsPage, CocktailDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<IngredientDetailsPage, IngredientDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<SurpriseMePage, SurpriseMePageViewModel>();
            containerRegistry.RegisterForNavigation<MyFavoritesPage, MyFavoritesPageViewModel>();
            containerRegistry.RegisterForNavigation<PlaygroundPage>();

            containerRegistry.Register<ICocktailService, CocktailService>();
            containerRegistry.RegisterSingleton<IIngredientsManager,IngredientsManager>();
            containerRegistry.RegisterSingleton<IGlassesManager,GlassesManager>();
            containerRegistry.RegisterSingleton<ICocktailsManager, CocktailsManager>();
            containerRegistry.RegisterSingleton<ICategoriesManager, CategoriesManager>();
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
