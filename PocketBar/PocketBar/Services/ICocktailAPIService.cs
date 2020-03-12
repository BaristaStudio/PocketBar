using PocketBar.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Services
{
    interface ICocktailAPIService
    {
        [Get("/filter.php?g={glass}")]
        Task<CocktailList> GetCocktailsByGlassAsync(string glass);

        [Get("/filter.php?c={category}")]
        Task<CocktailList> GetCocktailsByCategoryAsync(string category);

        [Get("/filter.php?a={alcoholic}")]
        Task<CocktailList> GetCocktailsByAlcoholicAsync(string alcoholic);

        [Get("/filter.php?i={ingredient}")]
        Task<CocktailList> GetCocktailsByIngredientAsync(string ingredient);

        [Get("/lookup.php?iid={ingredientId}")]
        Task<IngredientList> GetIngredientByIdAsync(string ingredientId);

        [Get("/lookup.php?i={drinkId}")]
        Task<CocktailList> GetCocktailByIdAsync(string drinkId);

        [Get("/random.php")]
        Task<CocktailList> GetRandomCocktailAsync();

        [Get("list.php?g=list")]
        Task<GlassList> GetAllGlassesAsync();

        [Get("list.php?i=list")]
        Task<GlassList> GetAllIngredientsAsync();

        [Get("list.php?c=list")]
        Task<GlassList> GetAllCategoriesAsync();

        [Get("list.php?a=list")]
        Task<GlassList> GetAllAlcoholicFiltersAsync();

    }
}
