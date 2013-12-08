using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using escoz;

namespace CoconutCalendarAdmin
{
	public partial class CoconutScheduleViewController : UIViewController
	{
		CoconutPickeDateWeekViewController _weekPick;
	

		public CoconutScheduleViewController () : base ("CoconutScheduleViewController", null)
		{

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.Title = "Schedule";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			var _addBtn = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			_addBtn.Clicked += (object sender, EventArgs e) => {

				this.NavigationController.PushViewController(new CoconutScheduleAddMenu(),true);
			};

			this.NavigationItem.RightBarButtonItem = _addBtn;

			locationBtn.BackgroundColor = UIColor.White;
			dateBtn.BackgroundColor = UIColor.White;


			var model = new CoconutCalendarScheduleLocationPickerModel (true);
			weekView.Hidden = true;
			


			// 1. setup data model for calendar
			model.LocationSelected += (object sender, EventArgs e) => {

				locationPicker.Hidden = true;
				CurrentQuery.sharedInstance().Location = model.SelectedLocation;
				locationBtn.SetTitle(model.SelectedLocation,UIControlState.Normal);

				_weekPick.setDate(CurrentQuery.sharedInstance().date);

				weekView.Hidden = false;
			};

			locationPicker.Model = model;


			if (_weekPick == null) {
				_weekPick =  new CoconutPickeDateWeekViewController (this.NavigationController);
				weekView.Add (_weekPick.View);
			}

		
			dateBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				var monthDate = new CoconutPickDateMonthViewController();
				this.NavigationController.PushViewController(monthDate, true);
				monthDate.DateOnChange += (object send, EventArgs ea) => {
					_weekPick.setDate(monthDate.selectedDate);
				};

			};

			locationBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				var locationModel = new CoconutCalendarScheduleLocationPickerModel (true);
				weekView.Hidden = true;


				// 1. setup data model for calendar
				locationModel.LocationSelected += (object sende, EventArgs er) => {

					locationPicker.Hidden = true;
					CurrentQuery.sharedInstance().Location = locationModel.SelectedLocation;
					locationBtn.SetTitle(locationModel.SelectedLocation,UIControlState.Normal);

					_weekPick.setDate(CurrentQuery.sharedInstance().date);

					weekView.Hidden = false;
				};

				locationPicker.Model = locationModel;
				locationPicker.Hidden = false;


				weekView.Hidden = true;
			};


			staffBtn.TouchUpInside += (object sender, EventArgs e) => {
				var staffModel = new CoconutCalendarScheduleLocationPickerModel (false);
				locationPicker.Model = staffModel;

			

				staffModel.StaffSelected+= (object ss, EventArgs ee) => {

					locationPicker.Hidden = true;

					CurrentQuery.sharedInstance().Staff = staffModel.SelectedStaff;

					staffBtn.SetTitle(staffModel.SelectedStaff,UIControlState.Normal);

					_weekPick.setDate(CurrentQuery.sharedInstance().date);

					weekView.Hidden = false;
				};

				locationPicker.Hidden = false;

				weekView.Hidden = true;
			};
		}
			
	
	}

	public class CurrentQuery
	{
		public string Location { set; get; }
		public string Staff { set; get; }

		public DateTime date { set; get; }

		private static CurrentQuery _cq;


		private CurrentQuery(){
		}

		public static CurrentQuery sharedInstance (){

			if (_cq == null){

				_cq = new CurrentQuery ();
			}
			return _cq;
		}

	}
}

