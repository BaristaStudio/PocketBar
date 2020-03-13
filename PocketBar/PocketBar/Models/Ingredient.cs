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
        public string Description { get; set; }

        [JsonProperty("strType")]
        public string Type { get; set; }

        [JsonProperty("strAlcohol")]
        public string Alcohol { get; set; }

        [JsonProperty("strABV")]
        public string ABV { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class IngredientList : INotifyPropertyChanged
    {
        [JsonProperty("drinks")]
        public IList<Ingredient> Ingredients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
