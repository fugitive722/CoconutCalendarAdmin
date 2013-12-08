
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CoconutCalendarAdmin; 

namespace escoz
{
	public class CalendarMonthViewController : UIViewController
    {

        public CalendarMonthView MonthView;
		public event EventHandler DateOnChange;
		public DateTime selectedDate;
        public override void ViewDidLoad()
        {
            MonthView = new CalendarMonthView();
			MonthView.OnDateSelected += (date) => {
				selectedDate = date;
				if (DateOnChange != null){
					DateOnChange(this,EventArgs.Empty);
				}
				Console.WriteLine(String.Format("Selected {0}", date.ToShortDateString()));
			};
			MonthView.OnFinishedDateSelection = (date) => {
				Console.WriteLine(String.Format("Finished selecting {0}", date.ToShortDateString()));
			};
			MonthView.IsDayMarkedDelegate = (date) => {

				foreach(var d in HttpClient.AppointmentList){

					if(d.date.Year == date.Year){
						if(d.date.DayOfYear == date.DayOfYear){
							Console.Out.WriteLine(date.Day);
							return true;
						}
					}


				}
				return false;
				//return (date.Day % 2==0) ? true : false;
			};

//			MonthView.IsDateAvailable = (date)=>{
//				return (date > DateTime.Today);
//			};


            View.AddSubview(MonthView);
        }
		
        

    }
}
