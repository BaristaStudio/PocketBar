using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using PocketBar.Controls.Common;
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

            var dashedLayer = new CAShapeLayer();
            var shapeRect = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height);
            dashedLayer.Bounds = shapeRect;
            dashedLayer.Position = new CGPoint(Bounds.Size.Width / 2, Bounds.Size.Height / 2);
            dashedLayer.FillColor = UIColor.Clear.CGColor;
            dashedLayer.StrokeColor = UIColor.Gray.CGColor;
            dashedLayer.LineWidth = 2;
            dashedLayer.LineJoin = CAShapeLayer.JoinRound;
            NSNumber[] patternArray = { 5, 5 };
            dashedLayer.LineDashPattern = patternArray;
            var path = new CGPath();
            path.MoveToPoint(CGPoint.Empty);
            path.AddLineToPoint(new CGPoint(Bounds.Size.Width, 0));
            dashedLayer.Path = path;
            Layer.AddSublayer(dashedLayer);
        }
    }
}