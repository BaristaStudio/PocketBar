using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PocketBar.Controls.Common;
using PocketBar.iOS.CustomRenders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace PocketBar.iOS.CustomRenders
{
    public class BorderlessEntryRenderer : EntryRenderer
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
                return;

            Control.BorderStyle = UITextBorderStyle.None;

        }
    }
}