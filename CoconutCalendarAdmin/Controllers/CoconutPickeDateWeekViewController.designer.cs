// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace CoconutCalendarAdmin
{
	[Register ("CoconutPickeDateWeekViewController")]
	partial class CoconutPickeDateWeekViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView calendarTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton frBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton moBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel monthLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton saBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton suBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton thBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton tuBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton weBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (calendarTableView != null) {
				calendarTableView.Dispose ();
				calendarTableView = null;
			}

			if (frBtn != null) {
				frBtn.Dispose ();
				frBtn = null;
			}

			if (moBtn != null) {
				moBtn.Dispose ();
				moBtn = null;
			}

			if (saBtn != null) {
				saBtn.Dispose ();
				saBtn = null;
			}

			if (suBtn != null) {
				suBtn.Dispose ();
				suBtn = null;
			}

			if (thBtn != null) {
				thBtn.Dispose ();
				thBtn = null;
			}

			if (tuBtn != null) {
				tuBtn.Dispose ();
				tuBtn = null;
			}

			if (weBtn != null) {
				weBtn.Dispose ();
				weBtn = null;
			}

			if (monthLabel != null) {
				monthLabel.Dispose ();
				monthLabel = null;
			}
		}
	}
}
