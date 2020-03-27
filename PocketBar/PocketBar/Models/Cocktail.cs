using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PocketBar.Models
{
    public class Cocktail : INotifyPropertyChanged
    {

        [JsonProperty("idDrink")]
        public string IdDrink { get; set; }

        [JsonProperty("strDrink")]
        public string DrinkName { get; set; }

        [JsonProperty("strDrinkAlternate")]
        public string DrinkAlternate { get; set; }

        [JsonProperty("strCategory")]
        public string Category { get; set; }

        [JsonProperty("strIBA")]
        public string IBA { get; set; }

        [JsonProperty("strAlcoholic")]
        public string Alcoholic { get; set; }

        [JsonProperty("strGlass")]
        public string Glass { get; set; }

        [JsonProperty("strInstructions")]
        public string Instructions { get; set; }

        [JsonProperty("strDrinkThumb")]
        public string DrinkThumb { get; set; }

        [JsonProperty("strIngredient1")]
        public string Ingredient1 { get; set; }

        [JsonProperty("strIngredient2")]
        public string Ingredient2 { get; set; }

        [JsonProperty("strIngredient3")]
        public string Ingredient3 { get; set; }

        [JsonProperty("strIngredient4")]
        public string Ingredient4 { get; set; }

        [JsonProperty("strIngredient5")]
        public string Ingredient5 { get; set; }

        [JsonProperty("strIngredient6")]
        public string Ingredient6 { get; set; }

        [JsonProperty("strIngredient7")]
        public string Ingredient7 { get; set; }

        [JsonProperty("strIngredient8")]
        public string Ingredient8 { get; set; }

        [JsonProperty("strIngredient9")]
        public string Ingredient9 { get; set; }

        [JsonProperty("strIngredient10")]
        public string Ingredient10 { get; set; }

        [JsonProperty("strIngredient11")]
        public string Ingredient11 { get; set; }

        [JsonProperty("strIngredient12")]
        public string Ingredient12 { get; set; }

        [JsonProperty("strIngredient13")]
        public string Ingredient13 { get; set; }

        [JsonProperty("strIngredient14")]
        public string Ingredient14 { get; set; }

        [JsonProperty("strIngredient15")]
        public string Ingredient15 { get; set; }

        [JsonProperty("strMeasure1")]
        public string Measure1 { get; set; }

        [JsonProperty("strMeasure2")]
        public string Measure2 { get; set; }

        [JsonProperty("strMeasure3")]
        public string Measure3 { get; set; }

        [JsonProperty("strMeasure4")]
        public string Measure4 { get; set; }

        [JsonProperty("strMeasure5")]
        public string Measure5 { get; set; }

        [JsonProperty("strMeasure6")]
        public string Measure6 { get; set; }

        [JsonProperty("strMeasure7")]
        public string Measure7 { get; set; }

        [JsonProperty("strMeasure8")]
        public string Measure8 { get; set; }

        [JsonProperty("strMeasure9")]
        public string Measure9 { get; set; }

        [JsonProperty("strMeasure10")]
        public string Measure10 { get; set; }

        [JsonProperty("strMeasure11")]
        public string Measure11 { get; set; }

        [JsonProperty("strMeasure12")]
        public string Measure12 { get; set; }

        [JsonProperty("strMeasure13")]
        public string Measure13 { get; set; }

        [JsonProperty("strMeasure14")]
        public string Measure14 { get; set; }

        [JsonProperty("strMeasure15")]
        public string Measure15 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<CocktailIngredient> _ingredients;

        public bool IsFavorite { get; set; }

        public List<CocktailIngredient> Ingredients
        {
            get
            {
                if(_ingredients == null)
                {
                    GetCocktailIngredients();
                }
                return _ingredients;
            }
        }

        private void GetCocktailIngredients()
        {
            this._ingredients  = new List<CocktailIngredient>();
            if (!string.IsNullOrEmpty(this.Ingredient1))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient1, this.Measure1));
            }
            if (!string.IsNullOrEmpty(this.Ingredient2))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient2, this.Measure2));
            }
            if (!string.IsNullOrEmpty(this.Ingredient3))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient3, this.Measure3));
            }
            if (!string.IsNullOrEmpty(this.Ingredient4))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient4, this.Measure4));
            }
            if (!string.IsNullOrEmpty(this.Ingredient5))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient5, this.Measure5));
            }
            if (!string.IsNullOrEmpty(this.Ingredient6))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient6, this.Measure6));
            }
            if (!string.IsNullOrEmpty(this.Ingredient7))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient7, this.Measure7));
            }
            if (!string.IsNullOrEmpty(this.Ingredient8))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient8, this.Measure8));
            }
            if (!string.IsNullOrEmpty(this.Ingredient9))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient9, this.Measure9));
            }
            if (!string.IsNullOrEmpty(this.Ingredient10))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient10, this.Measure10));
            }
            if (!string.IsNullOrEmpty(this.Ingredient11))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient11, this.Measure11));
            }
            if (!string.IsNullOrEmpty(this.Ingredient12))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient12, this.Measure12));
            }
            if (!string.IsNullOrEmpty(this.Ingredient13))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient13, this.Measure13));
            }
            if (!string.IsNullOrEmpty(this.Ingredient14))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient14, this.Measure14));
            }
            if (!string.IsNullOrEmpty(this.Ingredient15))
            {
                _ingredients.Add(new CocktailIngredient(this.Ingredient15, this.Measure15));
            }
        }
    }
    public class CocktailList : INotifyPropertyChanged
    {
        [JsonProperty("drinks")]
        public IList<Cocktail> Drinks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class CocktailIngredient
    {
        public CocktailIngredient(string ingredient, string measure)
        {
            Ingredient = ingredient;
            Measure = measure;
        }
        public string Ingredient { get; set; }
        public string Measure { get; set; }
    }

    public enum SearchType
    {
        Glass,
        Category,
        Ingredient
    }
}
