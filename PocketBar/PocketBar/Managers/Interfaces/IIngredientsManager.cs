using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketBar.Models;

namespace PocketBar.Managers.Interfaces
{
    public interface IIngredientsManager
    {
        Task<List<Ingredient>> GetIngredients();

        Task<Ingredient> GetRandomIngredient();

        Task<Ingredient> GetIngredient(int id);

        Task<Ingredient> GetIngredientByName(string ingredientName);

        Task<List<Cocktail>> GetCocktailsByIngredient(string ingredient);

        Task<Cocktail> GetRandomCocktailByIngredient(string ingredient);

	}
}
