using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CoconutCalendarAdmin
{
	public class CoconutCalendarScheduleLocationPickerModel : UIPickerViewModel
	{

		public string SelectedLocation{ get; set;}
		public string SelectedStaff { get; set;}

		public event EventHandler LocationSelected;
		public event EventHandler StaffSelected;


		Boolean isLocation = false;

		public CoconutCalendarScheduleLocationPickerModel (Boolean location)
		{
			isLocation = location;
		}

		public override int GetRowsInComponent (UIPickerView picker, int component)
		{
			if(isLocation){
				return HttpClient.LocationList.Count;
			}else{
				return HttpClient.StaffList.Count;
			}

		}

		public override int GetComponentCount (UIPickerView picker)
		{
			return 1;
		}

		public override string GetTitle (UIPickerView picker, int row, int component)
		{
			if (isLocation) {
				return HttpClient.LocationList [row].name;
			} else {
				return HttpClient.StaffList [row].first_name + " " + HttpClient.StaffList [row].last_name;
			}

		}
		private void OnLocationSelected()
		{
			if (LocationSelected != null) {
				LocationSelected (this, EventArgs.Empty);
			}
		}


		private void OnStaffSelected()
		{
			if (StaffSelected != null) {
				StaffSelected (this, EventArgs.Empty);
			}
		}
		public override void Selected (UIPickerView picker, int row, int component)
		{
			if (isLocation){

				SelectedLocation = HttpClient.LocationList[row].name;
				OnLocationSelected();
			}else{

				SelectedStaff = HttpClient.StaffList [row].last_name;
				OnStaffSelected ();
			}

		}
	}
}

