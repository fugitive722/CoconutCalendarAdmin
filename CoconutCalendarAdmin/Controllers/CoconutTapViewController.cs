using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace CoconutCalendarAdmin
{
	public class CoconutTapViewController : UITabBarController
	{
		UIViewController _scheduleVC;
		UIViewController _clientVC;

		public CoconutTapViewController ()
		{

		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
	
//			



			if (_scheduleVC == null) 
			{
				_scheduleVC = new UINavigationController(new CoconutScheduleViewController ());
				_scheduleVC.Title = "Schedule";
			}

			if (_clientVC == null)
			{
				_clientVC = new UINavigationController(new CoconutClientViewController());
				_clientVC.Title = "Client";
			}

			var tabs = new UIViewController [] {_scheduleVC,_clientVC};

			ViewControllers = tabs;

			SelectedViewController = _scheduleVC;
		}

	}
}

