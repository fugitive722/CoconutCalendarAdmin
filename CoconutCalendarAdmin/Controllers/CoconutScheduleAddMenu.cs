using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CoconutCalendarAdmin
{
	public partial class CoconutScheduleAddMenu : DialogViewController
	{
		public CoconutScheduleAddMenu () : base (UITableViewStyle.Grouped, null)
		{
			this.Pushing = true;
			Root = new RootElement ("Add Menu") {
				new Section ("Appointment"){
					new StringElement ("Client", () => {
						//new UIAlertView ("Hola", "Thanks for tapping!", null, "Continue").Show (); 
						this.NavigationController.PushViewController(new CoconutScheduleAddViewController(true),true);
					}),
					new StringElement ("Group", () => {
						//new UIAlertView ("Hola", "Thanks for tapping!", null, "Continue").Show (); 
						this.NavigationController.PushViewController(new CoconutScheduleAddViewController(false),true);
					}),
					//new EntryElement ("Name", "Enter your name", String.Empty)
				},
				new Section ("Absense"){
					new StringElement ("Personal", ()=>{
						this.NavigationController.PushViewController(new CoconutCalendarAbsebse("Personal"),true);
					}),
					new StringElement ("Sick", ()=>{
						this.NavigationController.PushViewController(new CoconutCalendarAbsebse("Sick"),true);
					}),
					new StringElement ("Vocation", ()=>{
						this.NavigationController.PushViewController(new CoconutCalendarAbsebse("Vocation"),true);
					}),
				},
			};
		}
	}
}
