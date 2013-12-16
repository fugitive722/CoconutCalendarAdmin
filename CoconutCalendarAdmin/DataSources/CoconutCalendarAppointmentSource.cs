using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using System.Json;

namespace CoconutCalendarAdmin
{
	public class CoconutCalendarAppointmentSource : UITableViewSource
	{
		List<Appointment> _appointmentList = new List<Appointment>();
		UINavigationController _navigation;
		string _currentDate;
		int _interval;
		DateTime _availableStart;
		DateTime _availableEnd;
		JsonValue _vendor;

		public CoconutCalendarAppointmentSource (List<Appointment> a, UINavigationController navi,string cur, string location, string staff)
		{
			_appointmentList = a;
			_navigation = navi;
			_currentDate = cur;

			if (location != "All Locations"){
				filterByLocation (location);
			}

			if (staff != "All Staffs" && staff != null){
				filterByStaff (staff);
			}
			getVendor ();
			getInterval ();

		}

		public void filterByLocation(string l){
		
			List<Appointment> location = new List<Appointment> ();

			foreach( var lc in _appointmentList){
				if ( l == lc.location.name){
					location.Add (lc);
				}
			}
			_appointmentList = location;
		}

		public void filterByStaff(string s){
		
			List<Appointment> staff = new List<Appointment> ();
			foreach( var sf in _appointmentList){
				if(sf.staff.last_name == s){
					staff.Add (sf);
				}
			}
			_appointmentList = staff;
		}

		#region implemented abstract members of UITableViewSource

		public override int RowsInSection (UITableView tableview, int section)
		{

			// Get from appointmetn->location->vendor->interval
			return 60 / _interval;

		}

		public override int NumberOfSections (UITableView tableView)
		{
			// Get from appointment-> location ->

			getAvailable ();

			//var hours = _availableStart.Hour - _availableEnd.Hour;
			return _availableStart.Hour - _availableEnd.Hour;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell") as CoconutCalendarTableCell;
			if (cell == null){
				var cellViews = NSBundle.MainBundle.LoadNib ("CoconutCalendarTableCell",tableView,null);
				cell = Runtime.GetNSObject (cellViews.ValueAt(0)) as CoconutCalendarTableCell;
			}

//			var start =_appointmentList[indexPath.Row].start.ToString().Split(' ');
//			var end = _appointmentList[indexPath.Row].end.ToString().Split(' ');
//			cell.start.Text = start[1];
//			cell.end.Text = end [1];
//			cell.staff.Text = _appointmentList[indexPath.Row].staff.first_name +" "+_appointmentList[indexPath.Row].staff.last_name;
//			cell.service.Text = _appointmentList[indexPath.Row].service.name;

			return cell;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "9:00";
		}
		#endregion

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
//			var selected = _appointmentList[indexPath.Row];
//
//			var appVC = new CoconutScheduleAddViewController (true);
//			appVC.initializeInput (_appointmentList[indexPath.Row]);
//			_navigation.PushViewController (appVC, true);
			//_navigation.PresentViewController (appVC,true,null);

		}


		private void getVendor (){
			var webclient = new HttpClient ();
			_vendor = webclient.getVendor ("");
		}

		private void getInterval (){
			_interval  = (int)(long)_vendor["interval"];
		}

		private void getAvailable(){
			if(_appointmentList.Count>0){
				var date = _appointmentList [0].date;
				var location = _appointmentList [0].location;
				//string dOpen = string.Format ("{0}_open",date.DayOfWeek.ToString ().ToLowerInvariant());
				//string dClose = string.Format ("{0}_closed",date.DayOfWeek.ToString ().ToLowerInvariant());
				switch(date.DayOfWeek.ToString()){
				case "Monday":

					var open = location.mondayOpen.Split(':');
					var close = location.mondayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
					break;
				case "Tuesday":
					var open1 = location.tuesdayOpen.Split(':');
					var close1 = location.tuesdayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open1[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close1[0]));
					break;
				case "Wednesday":
					var open2 = location.wednesdayOpen.Split(':');
					var close2 = location.wednesdayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
					break;
				case "Thursday":
					var open3 = location.thursdayOpen.Split(':');
					var close3 = location.thursdayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
					break;
				case "Friday":
					var open4 = location.fridayOpen.Split(':');
					var close4 = location.fridayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
				case "Saturday":
					var open5 = location.saturdayOpen.Split(':');
					var close5 = location.saturdayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
				case "Sunday":
					var open6 = location.sundayOpen.Split(':');
					var close6 = location.sundayClose.Split(':');

					_availableStart = new DateTime ().AddHours (Convert.ToDouble (open[0]));
					_availableEnd = new DateTime ().AddHours (Convert.ToDouble (close[0]));
					break;
				}

			}

		}

	}
}

