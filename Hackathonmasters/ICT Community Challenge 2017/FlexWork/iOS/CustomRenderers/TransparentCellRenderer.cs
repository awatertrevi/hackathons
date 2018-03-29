using System;
using Xamarin.Forms.Platform.iOS;
using FlexWork.Views.Cells;
using FlexWork.iOS.CustomRenderers;
using Xamarin.Forms;
using UIKit;

[assembly: ExportRenderer(typeof(TransparentCell), typeof(TransparentCellRenderer))]
namespace FlexWork.iOS.CustomRenderers
{
	public class TransparentCellRenderer : ViewCellRenderer
	{
		public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			cell.BackgroundColor = Color.Transparent.ToUIColor();
			cell.SelectedBackgroundView = new UIView();
			cell.BackgroundView = new UIView();

			tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			return cell;
		}
	}
}
