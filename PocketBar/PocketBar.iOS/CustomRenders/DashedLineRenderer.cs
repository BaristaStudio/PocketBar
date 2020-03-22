using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using PocketBar.Controls.CocktailDetails;
using PocketBar.iOS.CustomRenders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DashedLine), typeof(DashedLineRenderer))]
namespace PocketBar.iOS.CustomRenders
{
    class DashedLineRenderer : BoxRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var path = new UIBezierPath();
            path.MoveTo(new CGPoint(Bounds.Size.Width / 2, 0));
            path.AddLineTo(new CGPoint(Bounds.Size.Width / 2, Bounds.Size.Height));
            path.LineWidth = 6;

            var dashes = new[] { 0, path.LineWidth * 2 };
            path.SetLineDash(dashes, 0, dashes.Length, 0);
            path.LineCapStyle = CGLineCap.Round;

            (UIColor.White).SetStroke();
            path.Stroke();
        }
    }
}