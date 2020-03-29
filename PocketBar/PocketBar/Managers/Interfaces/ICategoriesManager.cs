using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketBar.Models;

namespace PocketBar.Managers.Interfaces
{
    public interface ICategoriesManager
    {
        Task<List<Category>> GetCategories();

        Task<List<Cocktail>> GetCocktailsByCategory(string category);
    }
}
