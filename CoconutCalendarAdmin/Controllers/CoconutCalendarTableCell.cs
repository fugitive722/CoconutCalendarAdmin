using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CoconutCalendarAdmin
{
	public partial class CoconutCalendarTableCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("CoconutCalendarTableCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CoconutCalendarTableCell");

		public CoconutCalendarTableCell (IntPtr handle) : base (handle)
		{
		}

		public static CoconutCalendarTableCell Create ()
		{
			return (CoconutCalendarTableCell)Nib.Instantiate (null, null) [0];
		}
	}
}

