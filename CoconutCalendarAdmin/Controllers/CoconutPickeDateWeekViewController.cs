using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CoconutCalendarAdmin
{

	public partial class CoconutPickeDateWeekViewController : UIViewController
	{

		UINavigationController _navi;
		public DateTime SelectedTime;
		private DateTime _lastSelectedTime;
		UISwipeGestureRecognizer _swipeLeft;
		UISwipeGestureRecognizer _swipeRight;
		Boolean _isSeleced = false;
		int selectedIndex = -1;

		DateTime[] _dates;
		public CoconutPickeDateWeekViewController (UINavigationController n) : base ("CoconutPickeDateWeekViewController", null)
		{
			_navi = n;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			_lastSelectedTime = new DateTime ();

			var currentDate = DateTime.Now;
			initializeDate (currentDate);

			if (_swipeLeft == null) {
				_swipeLeft = new UISwipeGestureRecognizer (this,new MonoTouch.ObjCRuntime.Selector("ViewSwipeLeft"));
				_swipeLeft.Direction = UISwipeGestureRecognizerDirection.Left;
				_swipeLeft.NumberOfTouchesRequired = 1;

			}

			if (_swipeRight == null) {
				_swipeRight = new UISwipeGestureRecognizer (this,new MonoTouch.ObjCRuntime.Selector("ViewSwipeRight"));
				_swipeRight.Direction = UISwipeGestureRecognizerDirection.Right;
				_swipeRight.NumberOfTouchesRequired = 1;
			}
		
			View.AddGestureRecognizer (_swipeLeft);
			View.AddGestureRecognizer (_swipeRight);

			moBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[0]);
				//new UIAlertView ("You selected", _dates[0].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[0];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				if(_isSeleced){
					deSeletedDate();
				}
				moBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[0].Month);
				selectedIndex = 0;
			};
			tuBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[1]);
				//new UIAlertView ("You selected", _dates[1].ToString(), null, "Ok", null).Show ();


				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[1];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);

				if(_isSeleced){
					deSeletedDate();
				}
				tuBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[1].Month);
				selectedIndex = 1;
			};
			weBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[2]);
				//new UIAlertView ("You selected", _dates[2].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[2];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				selectedIndex = 2;

				if(_isSeleced){
					deSeletedDate();
				}
				weBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[2].Month);
			};
			thBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[3]);
				//new UIAlertView ("You selected", _dates[3].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[3];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				selectedIndex = 3;
				if(_isSeleced){
					deSeletedDate();
				}
				thBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[3].Month);
			};
			frBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[4]);
				//new UIAlertView ("You selected", _dates[4].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[4];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				selectedIndex = 4;

				if(_isSeleced){
					deSeletedDate();
				}
				frBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[4].Month);
			};
			saBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[5]);
				//new UIAlertView ("You selected", _dates[5].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[5];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				selectedIndex = 5;

				if(_isSeleced){
					deSeletedDate();
				}
				saBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[5].Month);
			};
			suBtn.TouchUpInside += (object sender, EventArgs e) => {
				Console.Out.WriteLine(_dates[6]);
				//new UIAlertView ("You selected", _dates[6].ToString(), null, "Ok", null).Show ();
				_lastSelectedTime = SelectedTime;
				SelectedTime = _dates[6];
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource(SelectedTime);
				updateSelectedDate (SelectedTime);
				selectedIndex = 6;

				if(_isSeleced){
					deSeletedDate();
				}
				suBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[6].Month);
			};


			// setup calendar table view 
			setAppointmentTableSource (currentDate);


		}

		private void initializeDate(DateTime currentDate){

			updateMouthValue (currentDate.Month);


			if (_dates == null) {
				_dates = new DateTime[7];
			}

			var ddIndex = 0;

			//SelectedTime = currentDate;

			if(currentDate.DayOfWeek == DayOfWeek.Sunday){
				_dates [6] = currentDate;
				ddIndex = 6;
				suBtn.BackgroundColor = UIColor.LightGray;



			}else if(currentDate.DayOfWeek == DayOfWeek.Monday){
				_dates [0] = currentDate;
				ddIndex = 0;
				moBtn.BackgroundColor = UIColor.LightGray;

			}else if(currentDate.DayOfWeek == DayOfWeek.Tuesday){
				_dates [1] = currentDate;
				ddIndex = 1;
				tuBtn.BackgroundColor = UIColor.LightGray;

			}else if(currentDate.DayOfWeek == DayOfWeek.Wednesday){
				_dates [2] = currentDate;
				ddIndex = 2;
				weBtn.BackgroundColor = UIColor.LightGray;

			}else if(currentDate.DayOfWeek == DayOfWeek.Thursday){
				_dates [3] = currentDate;
				ddIndex = 3;
				thBtn.BackgroundColor = UIColor.LightGray;

			}else if(currentDate.DayOfWeek == DayOfWeek.Friday){
				_dates [4] = currentDate;
				ddIndex = 4;
				frBtn.BackgroundColor = UIColor.LightGray;

			}else if(currentDate.DayOfWeek == DayOfWeek.Saturday){
				_dates [5] = currentDate;
				ddIndex = 5;
				saBtn.BackgroundColor = UIColor.LightGray;
			}

			_isSeleced = true;
			_lastSelectedTime = currentDate;
			SelectedTime = currentDate;
			CurrentQuery.sharedInstance().date = SelectedTime;
			minusDay (ddIndex,currentDate);
			addDays (ddIndex,currentDate);

			printValue ();
			updateSelectedDate (SelectedTime);
			selectedIndex = ddIndex;


			//var calendarSource = CalendarTableViewSource ();
		}

		private void addDays(int num, DateTime cur)
		{
			for (int i=num+1; i< 7; i++)
			{
				var temp = cur;
				for (int j=num+1; j<i+1 ; j++){
					temp = temp.AddDays (1.0);
				}
				_dates [i] = temp;
			}
		}

		private void minusDay(int num, DateTime cur)
		{
			for (int i=0; i<num; i++) {
				var temp = cur;
				for (int j= 0; j<num-i; j++) {

					temp = temp.AddDays (-1.0);
				}
				_dates [i] = temp;
			}
		}

		[Export("ViewSwipeLeft")]
		protected void onViewSwipeLeft (UIGestureRecognizer sender){
			Console.Out.WriteLine ("Swipe Left !!!!");

			for (int i=0; i<7; i++){

				_dates [i] = _dates [i].AddDays (7.0);
			}

			printValue ();
			swipeUpdateSelectedDate (2);

		}

		[Export("ViewSwipeRight")]
		protected void onViewSwipeRight (UIGestureRecognizer sender){
			Console.Out.WriteLine ("Swipe Right !!!!");

			for (int i=0; i<7; i++){

				_dates [i] = _dates [i].AddDays (-7.0);
			}

			printValue ();
			swipeUpdateSelectedDate (1);
		}

		private void printValue()
		{
			moBtn.SetTitle(_dates[0].Day.ToString(), UIControlState.Normal); 
			tuBtn.SetTitle(_dates[1].Day.ToString(), UIControlState.Normal);
			weBtn.SetTitle(_dates[2].Day.ToString(), UIControlState.Normal);
			thBtn.SetTitle(_dates[3].Day.ToString(), UIControlState.Normal);
			frBtn.SetTitle(_dates[4].Day.ToString(), UIControlState.Normal);
			saBtn.SetTitle(_dates[5].Day.ToString(), UIControlState.Normal);
			suBtn.SetTitle(_dates[6].Day.ToString(), UIControlState.Normal);
	
		}

		private void updateMouthValue(int m){

			switch(m){
			case 1:
				monthLabel.Text = "January";
				break;
			case 2:
				monthLabel.Text = "February";
				break;
			case 3:
				monthLabel.Text = "March";
				break;
			case 4:
				monthLabel.Text = "April";
				break;
			case 5:
				monthLabel.Text = "May";
				break;
			case 6:
				monthLabel.Text = "June";
				break;
			case 7:
				monthLabel.Text = "July";
				break;
			case 8:
				monthLabel.Text = "August";
				break;
			case 9:
				monthLabel.Text = "September";
				break;
			case 10:
				monthLabel.Text = "October";
				break;
			case 11:
				monthLabel.Text = "November";
				break;
			case 12:
				monthLabel.Text = "December";
				break;
			}

		}

		private void deSeletedDate()
		{
			if(_lastSelectedTime.DayOfWeek == DayOfWeek.Monday){

				moBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Tuesday){
				tuBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Wednesday){
				weBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Thursday){
				thBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Friday){
				frBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Saturday){
				saBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}else if (_lastSelectedTime.DayOfWeek == DayOfWeek.Sunday){
				suBtn.BackgroundColor = UIColor.White;
				_isSeleced = false;
			}
		}

		private List<Appointment> dateAppointment(DateTime cur){

			var returnList = new List<Appointment> ();

			foreach (var a in HttpClient.AppointmentList){
			//var temp = cur.Subtract (a.date).Seconds;
				if(cur.DayOfYear == a.date.DayOfYear){
					returnList.Add (a);
				}
			}

			return returnList;

		}

		private void setAppointmentTableSource (DateTime d){
			var appointments = dateAppointment (d);

			var source = new CoconutCalendarAppointmentSource (appointments,_navi,SelectedTime.ToString(),CurrentQuery.sharedInstance().Location,CurrentQuery.sharedInstance().Staff);
			calendarTableView.Source = source;
			calendarTableView.RowHeight = 70f;
			calendarTableView.ReloadData ();
		}

		private void updateSelectedDate(DateTime s){
		
			setAppointmentTableSource (s);
		}

		private void swipeUpdateSelectedDate( int i){
		
			switch(i){
			case 1:
				// swipe right
				_lastSelectedTime = SelectedTime;
				deSeletedDate ();
				SelectedTime = _lastSelectedTime.AddDays (-7);
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource (SelectedTime);
				updateSelectedDate (SelectedTime);
				swipeUpdateButtonBackground ();
				break;

			case 2:
				// swipe left
				_lastSelectedTime = SelectedTime;
				deSeletedDate ();
				SelectedTime = _lastSelectedTime.AddDays (7);
				CurrentQuery.sharedInstance().date = SelectedTime;
				setAppointmentTableSource (SelectedTime);
				updateSelectedDate (SelectedTime);
				swipeUpdateButtonBackground ();
				break;
			}
		}


		private void swipeUpdateButtonBackground()
		{
			switch(selectedIndex){
			case 0:
				_isSeleced = true;
				moBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[0].Month);
				selectedIndex = 0;
				break;
			case 1:
				_isSeleced = true;
				tuBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[1].Month);
				selectedIndex = 1;
				break;
			case 2:
				_isSeleced = true;
				weBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[2].Month);
				selectedIndex = 2;
				break;
			case 3:
				_isSeleced = true;
				thBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[3].Month);
				selectedIndex = 3;
				break;
			case 4:
				_isSeleced = true;
				frBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[4].Month);
				selectedIndex = 4;
				break;
			case 5:
				_isSeleced = true;
				moBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[5].Month);
				selectedIndex = 5;
				break;
			case 6:
				_isSeleced = true;
				suBtn.BackgroundColor = UIColor.LightGray;
				_isSeleced = true;
				updateMouthValue(_dates[6].Month);
				selectedIndex = 6;
				break;

			}
		}

		public void setDate(DateTime d){
			updateMouthValue (d.Month);
			_lastSelectedTime = SelectedTime;
			deSeletedDate ();
			SelectedTime = d;
			CurrentQuery.sharedInstance().date = SelectedTime;
			initializeDate (SelectedTime);
			updateSelectedDate (SelectedTime);
		}

		public void setCurrentDate(){

			setDate (DateTime.Now);
		}




	
	}


}

