using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketBar.Models;

namespace PocketBar.Managers.Interfaces
{
	public interface ICocktailsManager
	{
		Task<List<Cocktail>> GetCocktails();
		Task<List<Cocktail>> FindCocktails(string term);

		Task<int> GetAlcoholicCocktails();

		Task<int> GetNonAlcoholicCocktails();

		Task<int> FillCocktailsWithOptionalAlcoholCocktails();

		Task<Cocktail> GetRandomCocktail();

		Task<Cocktail> GetRandomAlcoholicCocktail();

		Task<Cocktail> GetRandomNonAlcoholicCocktail();

		Task<Cocktail> GetCocktail(int id);

		List<Cocktail> GetFavorites();

		bool IsFavorite(int drinkId);

		void MarkAsFavorite(Cocktail cocktail);

		void RemoveFromFavorites(Cocktail cocktail);

		void MapCocktailFavorites(List<Cocktail> cocktails);
	}
}
