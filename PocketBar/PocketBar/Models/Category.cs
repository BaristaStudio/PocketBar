using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PocketBar.Models
{
    public class Category : INotifyPropertyChanged
    {

        [JsonProperty("strCategory")]
        public string CategoryName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class CategoryList : INotifyPropertyChanged
    {
        [JsonProperty("drinks")]
        public IList<Category> Categories { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
