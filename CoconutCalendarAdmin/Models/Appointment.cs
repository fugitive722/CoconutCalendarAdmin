using System;
using System.Collections.Generic;

namespace CoconutCalendarAdmin
{
	public class Appointment
	{

		public string id { set; get; }
		public string vendor_id { set; get; }
		public string uid { set; get; }
		public string location_id { set; get; }
		public string staff_id { set; get; }
		public string service_id { set; get; }
		public string status { set; get; }
		public string type { set; get; }
		public string limit { set; get; }
		public string note { set; get; }
		public string start { set; get; }
		public string end { set; get;}
		public string repeat_id { set; get; }
		public DateTime date { set; get; }

		public Location location { set; get;}
		public Staff staff { set; get; }
		public Services service { set; get; }

		public List<Client> client { set; get; }
		public List<ServiceNameTranslation> servicenametranslation { set; get; }
		public List<ServiceDesciptionTranslation> servicedesciptiontranslation { set; get; }


		public Appointment ()
		{
			this.client = new List<Client> ();
			this.servicenametranslation = new List<ServiceNameTranslation> ();
			this.servicedesciptiontranslation = new List<ServiceDesciptionTranslation> ();
		}
	}
}

