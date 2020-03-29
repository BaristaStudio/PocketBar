using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.Services
{
	public class CocktailService : ICocktailService
	{
		private ITheCocktailDBAPIService _apiService; 
		public ITheCocktailDBAPIService ApiService
		{
			get
			{
				return _apiService;
			}
		}

		public CocktailService()
		{
			_apiService = RestService.For<ITheCocktailDBAPIService>(Config.CocktailAPIURL);
		}

	}
}
