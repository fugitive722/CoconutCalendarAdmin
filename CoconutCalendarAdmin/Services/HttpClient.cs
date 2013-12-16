using System;
using System.Net;
using System.Json;
using System.Collections.Generic;


namespace CoconutCalendarAdmin
{
	public class HttpClient
	{

		//public static List<Location> LocationList = new List<Location>();
		public static List<Client> ClientList = new List<Client> ();
		public static List<Staff> StaffList = new List<Staff> ();
		//string url = "https://mobile.demo.coconutcalendar.com/api/1.1/";
		public static List<Appointment> AppointmentList = new List<Appointment>();
		public HttpClient ()
		{
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

		}

//		public void getLocations (string t)
//		{
//			var url = "https://mobile.demo.coconutcalendar.com/api/1.1/";
//			var req = "locations.json?";
//			t = "e484196ed3214e886fd0cbae1a342617866f34eb"; 
//			var token = string.Format("access_token={0}",t);
//			url = url + req + token;
//
//
//			Location all = new Location ();
//			all.name = "All Locations";
//
//			LocationList.Add (all);
//
//			Console.Out.WriteLine ("Location : " + url);
//			using (WebClient wc = new WebClient())
//			{
//				var response = wc.DownloadString (url);
//				var jsonResponse = JsonObject.Parse (response);
//
//				var locationJson = jsonResponse ["data"]["location"];
//
//				foreach (var l in locationJson) {
//					var location = new Location () {
//						id = ((JsonValue)l)["id"],
//						vendor_id = ((JsonValue)l)["vendor_id"],
//						name = ((JsonValue)l)["name"],
//						address =  ((JsonValue)l)["address"],
//						city = ((JsonValue)l)["city"],
//						country = ((JsonValue)l)["postal_zip"],
//						phone =  ((JsonValue)l)["phone"],
//						sundayOpen = ((JsonValue)l)["sunday_open"],
//						sundayClose = ((JsonValue)l)["sunday_closed"],
//						saturdayOpen = ((JsonValue)l)["saturday_open"],
//						saturdayClose = ((JsonValue)l)["saturday_closed"],
//						fridayOpen = ((JsonValue)l)["friday_open"],
//						fridayClose = ((JsonValue)l)["friday_closed"],
//						thursdayOpen = ((JsonValue)l)["thursday_open"],
//						thursdayClose = ((JsonValue)l)["thursday_closed"],
//						wednesdayOpen = ((JsonValue)l)["wednesday_open"],
//						wednesdayClose = ((JsonValue)l)["wednesday_closed"],
//						tuesdayOpen = ((JsonValue)l)["tuesday_open"],
//						tuesdayClose = ((JsonValue)l)["tuesday_closed"],
//						mondayOpen = ((JsonValue)l)["monday_open"],
//						mondayClose = ((JsonValue)l)["monday_closed"],
//					};
//	
//					var staffs = ((JsonValue)l) ["staff"];
//					foreach (var sf in staffs) {
//						var staff = new Staff (){
//							id = ((JsonValue)sf)["id"],
//							vendor_id = ((JsonValue)sf)["vendor_id"],
//							first_name = ((JsonValue)sf)["first_name"],
//							last_name = ((JsonValue)sf)["last_name"],
//							upload_id = ((JsonValue)sf)["upload_id"],
//							info = ((JsonValue)sf)["info"],
//							email = ((JsonValue)sf)["email"],
//							locationId = location.id,
//						};
//
//						location.locationStaff.Add (staff);
//					}
//
//					var services = ((JsonValue)l)["service"];
//					foreach(var ss in services){
//						var service = new Services (){
//							id = ((JsonValue)ss)["id"],
//							vendor_id = ((JsonValue)ss)["vendor_id"],
//							name = ((JsonValue)ss)["name"],
//							description = ((JsonValue)ss)["description"],
//							locationId = location.id,
//						};
//
//						location.LocationService.Add(service);
//					}
//
//						LocationList.Add(location);
//				}
//									
//			}
//		}

		public void getClients(string t){
			var req = "clients.json?";
			t = "e484196ed3214e886fd0cbae1a342617866f34eb"; 
			var token = string.Format("access_token={0}",t);
			var url = "https://mobile.demo.coconutcalendar.com/api/1.1/";
			url = url + req + token;
			Console.Out.WriteLine ("Clients : "+url);

			using (WebClient wc = new WebClient()){

				var response = wc.DownloadString (url);
				var jsonResponse = JsonObject.Parse (response);

				var clientJson = jsonResponse ["data"]["client"];

				foreach( var c in clientJson){

					var client = new Client (){
						id = ((JsonValue)c)["id"],
						vendor_id = ((JsonValue)c)["vendor_id"],
						address =  ((JsonValue)c)["address"],
						city = ((JsonValue)c)["city"],
						country = ((JsonValue)c)["country"],
						phone =  ((JsonValue)c)["phone"],
						cell_phone = ((JsonValue)c)["cell_phone"],
						work_phone = ((JsonValue)c)["work_phone"],
						firstName  = ((JsonValue)c)["first_name"],
						lastName  = ((JsonValue)c)["last_name"],
						prov_state =((JsonValue)c)["prov_state"],
						zip = ((JsonValue)c)["zip_postal"],
					};
					ClientList.Add (client);
				}

			}

		}

		public void getStaffs(string t){

			StaffList.Add (new Staff(){last_name = "All Staffs"});

			var req = "staff.json?";
			t = "e484196ed3214e886fd0cbae1a342617866f34eb"; 
			var token = string.Format("access_token={0}",t);
			var url = "https://mobile.demo.coconutcalendar.com/api/1.1/";
			url = url + req + token;


			using (WebClient wc = new WebClient()){

				var response = wc.DownloadString (url);
				var jsonResponse = JsonObject.Parse (response);

				var staffJson = jsonResponse ["data"]["staff"];

				foreach( var s in staffJson){

					var staff = new Staff (){
						id = ((JsonValue)s)["id"],
						vendor_id = ((JsonValue)s)["vendor_id"],
						first_name  = ((JsonValue)s)["first_name"],
						last_name  = ((JsonValue)s)["last_name"],
						info = ((JsonValue)s)["info"],
						email = ((JsonValue)s)["email"],
						locationId = ((JsonValue)s)["location"][0]["id"],
					};
					StaffList.Add (staff);
				}

			}

		}

		public JsonValue getVendor(string t){
			var req = "vendors.json?";
			t = "a3f746a4ec24ee0ab32e5a7c604fcf7e20284993&website_token=NDJjMzc0ZDliYzQxZWE0ODZkZTVmNmU0OTgwMGUyNmQ=";
			var token = string.Format ("access_token={0}",t);
			var url = "http://mobile.demo.coconutcalendar.com/api/1.1/";
			url = url + req + token;
			Console.Out.WriteLine ("Vendor : "+url);

			using (WebClient wc = new WebClient ()) {
				var response = wc.DownloadString (url);
				var jsonResponse = JsonObject.Parse (response);

				return jsonResponse["data"]["vendor"][0];
			};
		}

		public void getAppointments(string t){
		
			var req = "appointments.json?";
			t = "e484196ed3214e886fd0cbae1a342617866f34eb";
			var token = string.Format ("access_token={0}",t);
			var url = "http://mobile.demo.coconutcalendar.com/api/1.1/";
			url = url + req + token;
			Console.Out.WriteLine ("Appointment : "+url);

			using (WebClient wc = new WebClient()){

				var response = wc.DownloadString (url);
				var jsonResponse = JsonObject.Parse (response);

				var appointmenJson = jsonResponse["data"]["appointment"];

				foreach( var a in (JsonValue)appointmenJson){

					var appointment = new Appointment () {
						repeat_id =  ((JsonValue)a)["repeat_id"],
						id = ((JsonValue)a)["id"],
						vendor_id = ((JsonValue)a)["vendor_id"],
						uid = ((JsonValue)a)["uid"],
						location_id =((JsonValue)a)["location_id"] ,
						staff_id = ((JsonValue)a)["staff_id"],
						service_id = ((JsonValue)a)["service_id"],
						type = ((JsonValue)a)["type"],
						limit =  ((JsonValue)a)["limit"],
						note = ((JsonValue)a)["notes"],
						start = ((JsonValue)a)["start"],
						end = ((JsonValue)a)["end"],

					};

					var dates = appointment.start.Split ('-');
					var day = dates [2].Split (' ');
					var datetime = new DateTime (Convert.ToInt32(dates[0]),Convert.ToInt32(dates[1]),Convert.ToInt32(day[0]));
					appointment.date = datetime;

					var location = new Location () {
						id = ((JsonValue)a)["location"]["id"],
						vendor_id = ((JsonValue)a)["location"]["vendor_id"],
						name = ((JsonValue)a)["location"]["name"],
						address =  ((JsonValue)a)["location"]["address"],
						city = ((JsonValue)a)["location"]["city"],
						country = ((JsonValue)a)["location"]["postal_zip"],
						phone =  ((JsonValue)a)["location"]["phone"],
						sundayOpen = ((JsonValue)a)["location"]["sunday_open"],
						sundayClose = ((JsonValue)a)["location"]["sunday_closed"],
						saturdayOpen = ((JsonValue)a)["location"]["saturday_open"],
						saturdayClose = ((JsonValue)a)["location"]["saturday_closed"],
						fridayOpen = ((JsonValue)a)["location"]["friday_open"],
						fridayClose = ((JsonValue)a)["location"]["friday_closed"],
						thursdayOpen = ((JsonValue)a)["location"]["thursday_open"],
						thursdayClose = ((JsonValue)a)["location"]["thursday_closed"],
						wednesdayOpen = ((JsonValue)a)["location"]["wednesday_open"],
						wednesdayClose = ((JsonValue)a)["location"]["wednesday_closed"],
						tuesdayOpen = ((JsonValue)a)["location"]["tuesday_open"],
						tuesdayClose = ((JsonValue)a)["location"]["tuesday_closed"],
						mondayOpen = ((JsonValue)a)["location"]["monday_open"],
						mondayClose = ((JsonValue)a)["location"]["monday_closed"],
					};
					appointment.location = location;

					var staff = new Staff () {
						id = ((JsonValue)a)["staff"]["id"],
						vendor_id = ((JsonValue)a)["staff"]["vendor_id"],
						first_name = ((JsonValue)a)["staff"]["first_name"],
						last_name = ((JsonValue)a)["staff"]["last_name"],
						upload_id = ((JsonValue)a)["staff"]["upload_id"],
						info = ((JsonValue)a)["staff"]["info"],
						email = ((JsonValue)a)["staff"]["email"],
					 };
					appointment.staff = staff;

					var service = new Services () {
						id = ((JsonValue)a)["service"]["id"],
						vendor_id = ((JsonValue)a)["service"]["vendor_id"],
						name = ((JsonValue)a)["service"]["name"],
						description = ((JsonValue)a)["service"]["description"],
					};
					appointment.service = service;

					foreach (var c in ((JsonValue)a)["client"]){

						var client = new Client () {
							id = ((JsonValue)c) ["id"],
							vendor_id = ((JsonValue)c) ["vendor_id"],
							address = ((JsonValue)c) ["address"],
							city = ((JsonValue)c) ["city"],
							country = ((JsonValue)c) ["country"],
							phone = ((JsonValue)c) ["phone"],
							cell_phone = ((JsonValue)c) ["cell_phone"],
							work_phone = ((JsonValue)c) ["work_phone"],
							firstName = ((JsonValue)c) ["first_name"],
							lastName = ((JsonValue)c) ["last_name"],
							prov_state = ((JsonValue)c) ["prov_state"],
							zip = ((JsonValue)c) ["zip_postal"],
							walk_in = ((JsonValue)c) ["appointmentclient"]["walk_in"],
						};
						appointment.client.Add (client);
					}


					foreach(var st in((JsonValue)a)["servicenametranslation"] ){
						var trans = new ServiceNameTranslation () {

							id = ((JsonValue)st) ["id"],
							locale =  ((JsonValue)st) ["locale"],
							foreign_key = ((JsonValue)st) ["foreign_key"],
							field =  ((JsonValue)st) ["field"],
							content =  ((JsonValue)st) ["content"],
						};
						appointment.servicenametranslation.Add (trans);
					}

					foreach(var sd in ((JsonValue)a)["servicedescriptiontranslation"] ){

						var desc = new ServiceDesciptionTranslation () {
							id = ((JsonValue)sd) ["id"],
							locale =  ((JsonValue)sd) ["locale"],
							foreign_key = ((JsonValue)sd) ["foreign_key"],
							field =  ((JsonValue)sd) ["field"],
							content =  ((JsonValue)sd) ["content"],
						};
						appointment.servicedesciptiontranslation.Add (desc);
					}
					AppointmentList.Add (appointment);
				}
			}
		}


	}
}




































