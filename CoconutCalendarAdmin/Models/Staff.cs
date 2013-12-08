using System;

namespace CoconutCalendarAdmin
{
	public class Staff
	{
		public string id;
		public string vendor_id{ set; get; }
		public string upload_id{ set; get; }
		public string first_name{ set; get; }
		public string last_name{ set; get; }
		public string info{ set; get; }
		public string email{ set; get; }
		public string locationId { set; get; }

		public Staff ()
		{
		}
	}
}

