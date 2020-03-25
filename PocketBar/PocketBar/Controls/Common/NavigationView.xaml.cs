using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketBar.Controls.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationView : ContentView
    {
        public NavigationView()
        {
            InitializeComponent();
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(NavigationView),
            string.Empty);
        public DelegateCommand OnPressedBackCommand
        {
            get => (DelegateCommand)GetValue(OnPressedBackCommandProperty);
            set => SetValue(OnPressedBackCommandProperty, value);
        }
        public static readonly BindableProperty OnPressedBackCommandProperty = BindableProperty.Create(
            nameof(OnPressedBackCommand),
            typeof(DelegateCommand),
            typeof(NavigationView),
            null);
        public DelegateCommand ShareCocktailCommand
        {
            get => (DelegateCommand)GetValue(ShareCocktailCommandProperty);
            set => SetValue(ShareCocktailCommandProperty, value);
        }
        public static readonly BindableProperty ShareCocktailCommandProperty = BindableProperty.Create(
            nameof(ShareCocktailCommand),
            typeof(DelegateCommand),
            typeof(NavigationView),
            null);
        public bool HasShareButton
        {
            get => (bool)GetValue(HasShareButtonProperty);
            set => SetValue(HasShareButtonProperty, value);
        }
        public static readonly BindableProperty HasShareButtonProperty = BindableProperty.Create(
            nameof(HasShareButton),
            typeof(bool),
            typeof(NavigationView),
            false);
    }
}