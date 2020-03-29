using System;
namespace PocketBar.Services
{
    public interface ICocktailService
    {
        ITheCocktailDBAPIService ApiService { get; }
    }
}
