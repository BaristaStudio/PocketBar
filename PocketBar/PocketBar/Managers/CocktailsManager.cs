using MonkeyCache.FileStore;
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

		private const string AlcoholicFilter = "alcoholic";
		private const string NonAlcoholicFilter = "non_alcoholic";
		private const string OptionalAlcoholFilter = "optional_alcohol";

		private const string FavoritesListKey = "cocktails";
		public CocktailsManager(CocktailService service)
		{
			this.cocktailService = service;
			this.cocktails = new List<Cocktail>();
			this.alcoholicCocktails = new List<Cocktail>();
			this.nonAlcoholicCocktails = new List<Cocktail>();
			Barrel.ApplicationId = "PocketBar";
		}

		public async Task<List<Cocktail>> GetCocktails()
		{
			if (cocktails == null || cocktails.Count == 0)
			{
				try
				{
					var tasks = new[]
						{
					GetAlcoholicCocktails(),
					GetNonAlcoholicCocktails(),
					FillCocktailsWithOptionalAlcoholCocktails()
					};
					await Task.WhenAll(tasks);
					MapCocktailFavorites(cocktails);
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			return cocktails;
		}
		public async Task<List<Cocktail>> FindCocktails(string term)
		{
				try
				{
					var filteredCocktails = await cocktailService.ApiService.SearchCocktailByTermAsync(term);
					return (filteredCocktails.Drinks!=null) ? filteredCocktails.Drinks.OrderBy(c => c.DrinkName).ToList() : new List<Cocktail>();
				}
				catch(Exception e)
				{
					throw e;
				}
		}

		public async Task<int> GetAlcoholicCocktails()
		{
			if (alcoholicCocktails == null || alcoholicCocktails.Count == 0)
			{
				try
				{
					var response = await cocktailService.ApiService.GetCocktailsByAlcoholicAsync(AlcoholicFilter);
					alcoholicCocktails = response.Drinks.ToList();
					cocktails.AddRange(alcoholicCocktails);
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			return 0;
		}

		public async Task<int> GetNonAlcoholicCocktails()
		{
			if (nonAlcoholicCocktails == null || nonAlcoholicCocktails.Count == 0)
			{
				try
				{
					
				}
				catch(Exception e)
				{
					throw e;
				}
				var response = await cocktailService.ApiService.GetCocktailsByAlcoholicAsync(NonAlcoholicFilter);
				nonAlcoholicCocktails = response.Drinks.ToList();
				cocktails.AddRange(nonAlcoholicCocktails);
			}
			return 0;
		}

		public async Task<int> FillCocktailsWithOptionalAlcoholCocktails()
		{
			if (cocktails == null || cocktails.Count == 0)
			{
				try
				{
					var response = await cocktailService.ApiService.GetCocktailsByAlcoholicAsync(OptionalAlcoholFilter);
					cocktails.AddRange(response.Drinks.ToList());
					cocktails.OrderBy(c => c.DrinkName);
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			return 0;
		}

		public async Task<Cocktail> GetRandomCocktail()
		{
			try
			{
				var cocktail =  (await cocktailService.ApiService.GetRandomCocktailAsync()).Drinks.FirstOrDefault();
				var favorites = GetFavorites();
				if (favorites != null && favorites.Count > 0 && cocktail!=null)
				{
					cocktail.IsFavorite = favorites.Exists(f => f.IdDrink == cocktail.IdDrink);
				}
				return cocktail;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public async Task<Cocktail> GetRandomAlcoholicCocktail()
		{
			if (alcoholicCocktails == null || alcoholicCocktails.Count == 0)
			{
				try
				{
					await GetAlcoholicCocktails();
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			Random rand = new Random();
			int randomPosition = rand.Next(0, alcoholicCocktails.Count - 1);
			var cocktail =  alcoholicCocktails[randomPosition];
			var favorites = GetFavorites();
			if (favorites != null && favorites.Count > 0 && cocktail != null)
			{
				cocktail.IsFavorite = favorites.Exists(f => f.IdDrink == cocktail.IdDrink);
			}
			return cocktail;
		}
		public async Task<Cocktail> GetRandomNonAlcoholicCocktail()
		{
			if (nonAlcoholicCocktails == null || nonAlcoholicCocktails.Count == 0)
			{
				try
				{
					await GetNonAlcoholicCocktails();
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			Random rand = new Random();
			int randomPosition = rand.Next(0, nonAlcoholicCocktails.Count - 1);
			var cocktail = nonAlcoholicCocktails[randomPosition];
			var favorites = GetFavorites();
			if (favorites != null && favorites.Count > 0 && cocktail != null)
			{
				cocktail.IsFavorite = favorites.Exists(f => f.IdDrink == cocktail.IdDrink);
			}
			return cocktail;
		}

		public async Task<Cocktail> GetCocktail(int id)
		{
			try
			{
				var cocktailResponse = await cocktailService.ApiService.GetCocktailByIdAsync(id);
				var cocktail = cocktailResponse.Drinks.FirstOrDefault();
				var favorites = GetFavorites();
				if (favorites != null && favorites.Count > 0 && cocktail != null)
				{
					cocktail.IsFavorite = favorites.Exists(f => f.IdDrink == cocktail.IdDrink);
				}
				return cocktail;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		public List<Cocktail> GetFavorites()
		{
			return Barrel.Current.Get<List<Cocktail>>(FavoritesListKey);
		}
		public bool IsFavorite(int drinkId)
		{
			var favorites = GetFavorites();
			return favorites?.FirstOrDefault(x => x.IdDrink == drinkId.ToString()) != null;
		}
		public void MarkAsFavorite(Cocktail cocktail)
		{
			var cocktails = GetFavorites();
			if (cocktails == null)
				cocktails = new List<Cocktail>();
			cocktails.Add(cocktail);
			cocktail.IsFavorite = true;
			AddToBarrel(cocktails);
		}
		public void RemoveFromFavorites(Cocktail cocktail)
		{
			var cocktails = GetFavorites();
			if (cocktails == null)
				return;
			cocktails.Remove(cocktails.FirstOrDefault(c => c.IdDrink == cocktail.IdDrink));
			cocktail.IsFavorite = false;
			AddToBarrel(cocktails);
		}
		void AddToBarrel(List<Cocktail> cocktails)
		{
			if (Barrel.Current.Exists(FavoritesListKey))
				Barrel.Current.Empty(FavoritesListKey);

			Barrel.Current.Add(FavoritesListKey, cocktails, TimeSpan.FromDays(3));
		}

		public void MapCocktailFavorites(List<Cocktail> cocktails)
		{
			var favorites = GetFavorites();
			if (favorites != null && favorites.Count > 0 && cocktails!=null && cocktails.Count >0)
			{
				cocktails.ForEach(c => c.IsFavorite = favorites.Exists(f => f.IdDrink == c.IdDrink));
			}
		}
	}
}
