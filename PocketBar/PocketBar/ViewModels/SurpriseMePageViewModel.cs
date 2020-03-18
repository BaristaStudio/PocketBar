using PocketBar.Managers;
using PocketBar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PocketBar.ViewModels
{
    class SurpriseMePageViewModel: BaseViewModel
    {
        private CocktailsManager cocktailsManager;

        private IngredientsManager ingredientsManager;

        private GlassesManager glassesManager;
        public Cocktail RandomCocktail { get; set; }
        public Cocktail RandomAlcoholicCocktail { get; set; }
        public Cocktail RandomNonAlcoholicCocktail { get; set; }
        public Ingredient RandomIngredient { get; set; }
        public Glass RandomGlass { get; set; }
        public bool IsFiltered { get; set; }
        public ObservableCollection<Cocktail> FilteredCocktails { get; set; }

        public SurpriseMePageViewModel(CocktailsManager cocktailsManager, IngredientsManager ingredientsManager, GlassesManager glassesManager)
        {
            this.cocktailsManager = cocktailsManager;
            this.glassesManager = glassesManager;
            this.ingredientsManager = ingredientsManager;
            this.IsFiltered = false;
        }

        public async void GetData()
        {
            IsLoading = true;
            this.RandomCocktail = await cocktailsManager.GetRandomCocktail();
            this.RandomAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
            this.RandomNonAlcoholicCocktail = await cocktailsManager.GetRandomAlcoholicCocktail();
            this.RandomIngredient = await ingredientsManager.GetRandomIngredient();
            this.RandomGlass = await glassesManager.GetRandomGlass();
            IsLoading = false;
        }

        public async void Search(string searchTerm)
        {
            IsLoading = true;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                IsFiltered = true;
                FilteredCocktails = new ObservableCollection<Cocktail>(await cocktailsManager.FindCocktails(searchTerm));
            }
            else
            {
                IsFiltered = false;
            }
            IsLoading = false;
        }
    }
}
