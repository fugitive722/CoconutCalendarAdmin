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
	[Register ("CoconutCalendarTableCell")]
	partial class CoconutCalendarTableCell
	{
		[Outlet]
		public MonoTouch.UIKit.UILabel end { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel service { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel staff { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel start { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (start != null) {
				start.Dispose ();
				start = null;
			}

			if (end != null) {
				end.Dispose ();
				end = null;
			}

			if (service != null) {
				service.Dispose ();
				service = null;
			}

			if (staff != null) {
				staff.Dispose ();
				staff = null;
			}
		}
	}
}
