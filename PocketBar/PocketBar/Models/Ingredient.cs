using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PocketBar.Models
{
    public class Ingredient : INotifyPropertyChanged
    {

        [JsonProperty("idIngredient")]
        public string IdIngredient { get; set; }

        [JsonProperty("strIngredient")]
        public string IngredientName { get; set; }

        [JsonProperty("strIngredient1")]
        private string IngredientName2 { set { IngredientName = value; } }

        [JsonProperty("strDescription")]
        private string _description { get; set; }
        public string Description
        {
            get { return _description ?? ""; } 
        }

        [JsonProperty("strAlcohol")]
        private string _alcohol { get; set; }
        public string Alcohol { get { return _alcohol == null ? "Non Alcoholic" : "Alcoholic"; ; } }

        [JsonProperty("strABV")]
        private string _ABV { get; set; }
        public string ABV {get{return _ABV ?? ""; }}



        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class IngredientList : INotifyPropertyChanged
    {
        [JsonProperty("drinks")]
        public IList<Ingredient> Ingredients { get; set; }

        [JsonProperty("ingredients")]
        private IList<Ingredient> Ingredients2 { set { Ingredients = value;  } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
