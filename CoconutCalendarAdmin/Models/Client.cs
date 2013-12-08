using System;

namespace CoconutCalendarAdmin
{
	public class Client : IComparable<Client>
	{
		public string id { set; get; }
		public string vendor_id { set; get; }
		public string firstName { set; get; }
		public string lastName { set; get; }
		public string email { set; get; }
		public string phone { set; get; }
		public string cell_phone { set; get; }
		public string work_phone { set; get;}
		public string notes { set; get; }
		public string address { set; get; }
		public string city { set; get; }
		public string prov_state { set; get; }
		public string country { set; get;}
		public string zip { set; get; }
		public string walk_in { set; get; }

		public Client (){}

		public int CompareTo(Client c){
		
			return this.lastName.CompareTo (c.lastName);
		} 
	}
}

