using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketBar.Models;

namespace PocketBar.Managers.Interfaces
{
    public interface IGlassesManager
    {
        Task<List<Glass>> GetGlasses();

        Task<Glass> GetRandomGlass();

        Task<List<Cocktail>> GetCocktailsByGlass(string glass);

        Task<Cocktail> GetRandomCocktailByGlass(string glass);
    }
}
