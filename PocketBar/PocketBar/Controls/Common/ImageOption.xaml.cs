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
	public partial class ImageOption : ContentView
	{
		public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(ImageOption), default(string));
		public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSrc), typeof(ImageSource), typeof(ImageOption), default(ImageSource));
		public static readonly BindableProperty OptionSelectedCommandProperty = BindableProperty.Create(nameof(OptionSelectedCommand), typeof(DelegateCommand<ImageOption>), typeof(ImageOption), default(DelegateCommand));

		public string OptionName { get => (string)GetValue(OptionNameProperty); set => SetValue(OptionNameProperty, value); }
		public ImageSource ImageSrc { get => (ImageSource)GetValue(ImageSourceProperty); set => SetValue(ImageSourceProperty, value); }
		public DelegateCommand<ImageOption> OptionSelectedCommand { get => (DelegateCommand<ImageOption>)GetValue(OptionSelectedCommandProperty); set => SetValue(OptionSelectedCommandProperty, value); }
		public ImageOption()
		{
			InitializeComponent();
		}
	}
}