using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace CoconutCalendarAdmin
{
	public class CoconutCalendarAppointmentSource : UITableViewSource
	{
		List<Appointment> _appointmentList = new List<Appointment>();
		UINavigationController _navigation;
		string _currentDate;

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
			return _appointmentList.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell") as CoconutCalendarTableCell;
			if (cell == null){
				var cellViews = NSBundle.MainBundle.LoadNib ("CoconutCalendarTableCell",tableView,null);
				cell = Runtime.GetNSObject (cellViews.ValueAt(0)) as CoconutCalendarTableCell;
			}

			var start =_appointmentList[indexPath.Row].start.ToString().Split(' ');
			var end = _appointmentList[indexPath.Row].end.ToString().Split(' ');
			cell.start.Text = start[1];
			cell.end.Text = end [1];
			cell.staff.Text = _appointmentList[indexPath.Row].staff.first_name +" "+_appointmentList[indexPath.Row].staff.last_name;
			cell.service.Text = _appointmentList[indexPath.Row].service.name;

			return cell;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return _currentDate;

		}
		#endregion

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var selected = _appointmentList[indexPath.Row];

			var appVC = new CoconutScheduleAddViewController (true);
			appVC.initializeInput (_appointmentList[indexPath.Row]);
			_navigation.PushViewController (appVC, true);
			//_navigation.PresentViewController (appVC,true,null);


		}
	}
}

