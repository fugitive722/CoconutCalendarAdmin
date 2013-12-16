using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CoconutCalendarAdmin
{
	public partial class CoconutCalendarScheduleAddNewClient : DialogViewController
	{
		public event EventHandler finishedSelect;
		public Client selectedClient;
		RadioGroup _radioG;
		List<Section> _allSection;

		public CoconutCalendarScheduleAddNewClient () : base (UITableViewStyle.Plain, null)
		{
			this.Pushing = true;
			this.EnableSearch = true;

			var addBtn = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			addBtn.Clicked += (object sender, EventArgs e) => {
				this.NavigationController.PushViewController(new CoconutClientAddViewController(null),true);
			};
			this.NavigationItem.RightBarButtonItem = addBtn;


			_radioG = new RadioGroup ("addclient", 0);
			_allSection = new List<Section> ();

			Root = new RootElement ("Add New Client",_radioG);
			//createClientStringElement ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			createClientStringElement ();

		}
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if(finishedSelect != null){
				finishedSelect (this, EventArgs.Empty);
			}
		}
		public override void FinishSearch ()
		{
			base.FinishSearch ();
			Title = "Secleted : "+ selectedClient.firstName +" "+selectedClient.lastName;
		}

		public List<Client> sortNamesbyAlphebica ()
		{
			HttpClient.ClientList.Sort ();
			return HttpClient.ClientList;
		}

		public void createClientStringElement()
		{
			cleanAllClienStringElement ();
			var s = createSection();

			var index = '0';
			foreach( var client in sortNamesbyAlphebica()){
				if (index == client.lastName [0]) {
					var strE = new StyledStringElement (client.firstName+" "+client.lastName);
					strE.Tapped += () => {
						Title = "Selcete : "+client.firstName+" "+client.lastName;
						selectedClient =client;
					};
					s.Add (strE);

				} else {
					index = client.lastName [0];
					s = new Section (client.lastName[0].ToString().ToUpper());
					s = createSection (client.lastName[0].ToString().ToUpper());
					var strE = new StyledStringElement (client.firstName+" "+client.lastName);
					strE.Tapped += () => {
						Title = "Selceted : "+client.firstName+" "+client.lastName;
						selectedClient =client;
					};
					s.Add (strE);
					Root.Add (s);
				}

			}
		}

		public Section createSection(string name =null){

			var s = new Section (name);
			_allSection.Add (s);
			return s;
		}

		public void cleanAllClienStringElement(){
			for (int i=0; i<_allSection.Count; i++){
				_allSection.RemoveAt (i);
			}
			Root.Clear ();
		}

	}

}
