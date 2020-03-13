using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketBar.Services
{
	public class CocktailService
	{
		private ICocktailAPIService _apiService; 
		public ICocktailAPIService ApiService
		{
			get
			{
				return _apiService;
			}
		}

		public CocktailService()
		{
			_apiService = RestService.For<ICocktailAPIService>(Config.CocktailAPIURL);
		}

	}
}
