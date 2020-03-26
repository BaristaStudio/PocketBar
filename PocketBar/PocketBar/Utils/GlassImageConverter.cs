using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PocketBar.Utils
{
    public class GlassImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = null;
            string name = value.ToString();
            switch (name)
            {
                case "Margarita/Coupette glass":
                    image = "margaritaCoupetteGlass";
                    break;
                case "Nick and Nora Glass":
                    image = "nickNoraGlass";
                    break;
                case "Old-fashioned glass":
                    image = "oldFGlass";
                    break;
                default:
                    image = new string(name.ToLower()
                    .Select((x, i) => (i != 0 && char.IsWhiteSpace(name[i - 1])) ? char.ToUpper(x) : x).ToArray()).Replace(" ", string.Empty);
                    break;
            }
            return $"{image}.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
