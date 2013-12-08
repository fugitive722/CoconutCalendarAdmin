// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CoconutCalendarAdmin
{
	[Register ("CoconutScheduleViewController")]
	partial class CoconutScheduleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView calendarTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton dateBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton locationBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPickerView locationPicker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton staffBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView weekView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (calendarTableView != null) {
				calendarTableView.Dispose ();
				calendarTableView = null;
			}

			if (dateBtn != null) {
				dateBtn.Dispose ();
				dateBtn = null;
			}

			if (locationBtn != null) {
				locationBtn.Dispose ();
				locationBtn = null;
			}

			if (locationPicker != null) {
				locationPicker.Dispose ();
				locationPicker = null;
			}

			if (weekView != null) {
				weekView.Dispose ();
				weekView = null;
			}

			if (staffBtn != null) {
				staffBtn.Dispose ();
				staffBtn = null;
			}
		}
	}
}
