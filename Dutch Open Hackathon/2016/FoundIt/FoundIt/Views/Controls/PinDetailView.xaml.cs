using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FoundIt.Views.Controls
{
	public partial class PinDetailView : ContentView
	{

        public string Title { get { return this.lblTitle.Text; } set { this.lblTitle.Text = value; } }
        public string Subtitle { get { return this.lblSubtitle.Text; } set { this.lblSubtitle.Text = value; } }

        public ImageSource ImageSource { get { return this.imgImage.Source; } set { this.imgImage.Source = value; } }

        public PinDetailView ()
		{
			InitializeComponent ();   
		}
	}

    public class PinDetailView2 : RelativeLayout
    {
        public PinDetailView2()
        {
            Image img;
            Label lbl1;
            Label lbl2;
            this.Children.Add(img = new Image(),
                widthConstraint: Constraint.Constant(30),
                heightConstraint: Constraint.RelativeToParent(l => l.Height)
            );
            this.Children.Add(lbl1 = new Label() { Text = "Title" },
                xConstraint: Constraint.RelativeToView(img, (l, v) => v.Bounds.Right),
                widthConstraint: Constraint.RelativeToView(img, (l, v) => l.Width - v.Bounds.Right),
                heightConstraint: Constraint.RelativeToView(img, (l, v) => lbl1.Measure(l.Width - v.Bounds.Right, l.Height).Request.Height)
            );
            this.Children.Add(lbl2 = new Label() { Text = "Subtitle" },
                xConstraint: Constraint.RelativeToView(img, (l, v) => v.Bounds.Right),
                yConstraint: Constraint.RelativeToView(lbl1, (l, v) => v.Bounds.Bottom),
                widthConstraint: Constraint.RelativeToView(img, (l, v) => l.Width - v.Bounds.Right),
                heightConstraint: Constraint.RelativeToView(lbl1, (l, v) => lbl2.Measure(l.Width - v.Bounds.Right, l.Height - v.Height).Request.Height)
            );
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {

            var m1 = this.Children.OfType<Label>().ElementAt(0).Measure(double.PositiveInfinity, double.PositiveInfinity).Request;
            var m2 = this.Children.OfType<Label>().ElementAt(0).Measure(double.PositiveInfinity, double.PositiveInfinity).Request;

            if (double.IsPositiveInfinity(widthConstraint))
                widthConstraint = Math.Max(m1.Width, m2.Width) + 30;

            if (double.IsPositiveInfinity(heightConstraint))
                heightConstraint = Math.Max(30, m1.Height + m2.Height);

            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
    
}
