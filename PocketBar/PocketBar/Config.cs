using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar
{
	public static class Config
	{
		public const string CocktailAPIHost = "https://www.thecocktaildb.com/api/json";
		public const string CocktailAPIVersion = "V1";
		public const string CocktailAPIKey = "1";
		public static string CocktailAPIURL { get 
			{ 
				return $"{CocktailAPIHost}/{CocktailAPIVersion}/{CocktailAPIKey}"; 
			} 
		}
	}
}
