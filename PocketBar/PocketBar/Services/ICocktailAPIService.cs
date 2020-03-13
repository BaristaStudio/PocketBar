using PocketBar.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Services
{
    public interface ICocktailAPIService
    {
         [Get("/filter.php?g={glass}")]
         Task<Cocktail> GetCocktailsByGlassAsync(string glass);

         [Get("/filter.php?c={category}")]
         Task<Cocktail> GetCocktailsByCategoryAsync(string category);

         [Get("/filter.php?a={alcoholic}")]
         Task<Cocktail> GetCocktailsByAlcoholicAsync(string alcoholic);

         [Get("/filter.php?i={ingredient}")]
         Task<Cocktail> GetCocktailsByIngredientAsync(string ingredient);

         [Get("/lookup.php?iid={ingredientId}")]
         Task<IngredientList> GetIngredientByIdAsync(string ingredientId);

         [Get("/lookup.php?i={drinkId}")]
         Task<Cocktail> GetCocktailByIdAsync(string drinkId);

        [Get("/random.php")]
        Task<Cocktail> GetRandomCocktailAsync();

        [Get("/list.php?g=list")]
        Task<GlassList> GetAllGlassesAsync();

        [Get("/list.php?i=list")]
        Task<IngredientList> GetAllIngredientsAsync();

        [Get("/list.php?c=list")]
        Task<GlassList> GetAllCategoriesAsync();

        [Get("/list.php?a=list")]
        Task<GlassList> GetAllAlcoholicFiltersAsync();

    }
}
