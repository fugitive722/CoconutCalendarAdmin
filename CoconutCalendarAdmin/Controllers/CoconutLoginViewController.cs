using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MBProgressHUD;
using System.Threading.Tasks;

namespace CoconutCalendarAdmin
{
	public partial class CoconutLoginViewController : DialogViewController
	{
		public CoconutLoginViewController () : base (UITableViewStyle.Grouped, null)
		{
			var hud = new MTMBProgressHUD (View) {
				LabelText = "Waiting...",
				RemoveFromSuperViewOnHide = true
			};
			View.AddSubview (hud);
			var nlSection = new Section ("Coconut Calendar Login"){
				new EntryElement ("Username", "Enter your username", String.Empty),
				new EntryElement ("Password", "Enter your password", String.Empty),
			};

			var loginSection = new Section () {


				new StringElement("Login", ()=>{

					hud.Show(animated:true);
					Task.Factory.StartNew(()=>{
						var webReq = new HttpClient();
						webReq.getLocations("test");
						webReq.getClients("test");
						webReq.getAppointments("test");
						webReq.getStaffs("test");

					}).ContinueWith(
						t =>{

						hud.Hide(animated: true, delay: 0);
						Console.Out.WriteLine(HttpClient.AppointmentList);
						var tapVC = new CoconutTapViewController();
						this.PresentViewController(tapVC,true,null);
					},TaskScheduler.FromCurrentSynchronizationContext());


				}),
			};

			Root = new RootElement ("CoconutLoginViewController");
			Root.Add (nlSection);
			Root.Add (loginSection);

		}
	}
}











