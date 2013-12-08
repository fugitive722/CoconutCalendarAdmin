using System;
using System.Collections.Generic;

namespace CoconutCalendarAdmin
{
	public class Location
	{
		public string id { set; get; }
		public string vendor_id { set; get; }
		public string name { set; get;}
		public string address { set; get; }
		public string city { set; get; }
		public string country { set; get; }
		public string postalzip { set; get; }
		public string phone { set; get; }
		public string sundayOpen { set; get; }
		public string sundayClose { set; get;}
		public string mondayOpen { set; get; }
		public string mondayClose { set; get;}
		public string tuesdayOpen { set; get; }
		public string tuesdayClose { set; get;}
		public string wednesdayOpen { set; get; }
		public string wednesdayClose { set; get;}
		public string thursdayOpen { set; get; }
		public string thursdayClose { set; get;}
		public string fridayOpen { set; get; }
		public string fridayClose { set; get;}
		public string saturdayOpen { set; get; }
		public string saturdayClose { set; get;}

		public List <Staff> locationStaff;
		public List <Services> LocationService;

		public Location ()
		{
			locationStaff = new List<Staff> ();
			LocationService = new List<Services> ();
		}
	}
}

