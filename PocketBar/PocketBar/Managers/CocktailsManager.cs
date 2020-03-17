using PocketBar.Models;
using PocketBar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBar.Managers
{
	public class CocktailsManager
	{
		private CocktailService cocktailService;
		private List<Cocktail> cocktails;
		private List<Cocktail> alcoholicCocktails;
		private List<Cocktail> nonAlcoholicCocktails;

		private const string AlcoholicFilter = "Alcoholic";
		private const string NonAlcoholicFilter = "Non alcoholic";

		public CocktailsManager(CocktailService service)
		{
			this.cocktailService = service;
		}

		public async Task<List<Cocktail>> GetCocktails()
		{
			if (cocktails == null || cocktails.Count == 0)
			{
			}
			return cocktails;
		}
		public async Task<List<Cocktail>> FindCocktails(string term)
		{
			if (cocktails == null || cocktails.Count == 0)
			{
				await GetCocktails();
			}
			return cocktails.Where(c => c.DrinkName.Contains(term)).ToList();
		}

		public async Task<int> GetAlcoholicCocktails()
		{
			if (alcoholicCocktails == null || alcoholicCocktails.Count == 0)
			{
				var response =  await cocktailService.ApiService.GetCocktailsByAlcoholicAsync(AlcoholicFilter);
				alcoholicCocktails = response.Drinks.ToList();
			}
			return 0;
		}

		public async Task<int> GetNonAlcoholicCocktails()
		{
			if (nonAlcoholicCocktails == null || nonAlcoholicCocktails.Count == 0)
			{
				var response = await cocktailService.ApiService.GetCocktailsByAlcoholicAsync(NonAlcoholicFilter);
				nonAlcoholicCocktails = response.Drinks.ToList();
			}
			return 0;
		}

		public async Task<Cocktail> GetRandomCocktail()
		{
			return await cocktailService.ApiService.GetRandomCocktailAsync();
		}
		public async Task<Cocktail> GetRandomAlcoholicCocktail()
		{
			if (alcoholicCocktails == null || alcoholicCocktails.Count == 0)
			{
				await GetAlcoholicCocktails();
			}

			Random rand = new Random();
			int randomPosition = rand.Next(0, alcoholicCocktails.Count - 1);
			return alcoholicCocktails[randomPosition];
		}
		public async Task<Cocktail> GetRandomNonAlcoholicCocktail()
		{
			if (nonAlcoholicCocktails == null || nonAlcoholicCocktails.Count == 0)
			{
				await GetNonAlcoholicCocktails();
			}

			Random rand = new Random();
			int randomPosition = rand.Next(0, nonAlcoholicCocktails.Count - 1);
			return nonAlcoholicCocktails[randomPosition];
		}

		public async Task<Cocktail> GetCocktail(int id)
		{
			var cocktail = await cocktailService.ApiService.GetCocktailByIdAsync(id);
			return cocktail;
		}
	}
}
