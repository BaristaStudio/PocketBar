using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PocketBar.Controls.CocktailDetails;
using PocketBar.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DashedLine), typeof(DashedLineRenderer))]
namespace PocketBar.Droid.CustomRenders
{
    class DashedLineRenderer : BoxRenderer
    {
        public DashedLineRenderer(Context context) : base(context)
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            var paint = new Paint { StrokeWidth = 2, AntiAlias = true };
            paint.SetStyle(Paint.Style.Stroke);
            paint.SetPathEffect(new DashPathEffect(new[] { 6 * this.Context.Resources.DisplayMetrics.Density, 2 * this.Context.Resources.DisplayMetrics.Density }, 0));

            var p = new Path();
            p.LineTo(canvas.Width, 0);
            canvas.DrawPath(p, paint);
        }
    }
}