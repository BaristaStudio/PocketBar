using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PocketBar.Models
{
    public class Glass : INotifyPropertyChanged
    {

        [JsonProperty("strGlass")]
        public string GlassName { get; set; }

        [JsonProperty("strCategory")]
        public string Category { get; set; }

        [JsonProperty("strIngredient1")]
        public string Ingredient { get; set; }

        [JsonProperty("strAlcoholic")]
        public string Alcoholic { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class GlassList : INotifyPropertyChanged
    {
        [JsonProperty("drinks")]
        public IList<Glass> Drinks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
