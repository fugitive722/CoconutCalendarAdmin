using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CoconutCalendarAdmin
{
	public partial class CoconutClientViewController : DialogViewController
	{
		public CoconutClientViewController () : base (UITableViewStyle.Plain, null)
		{
			this.EnableSearch = true;
			var nameList = sortNamesbyAlphebica ();
			var _addBtn = new UIBarButtonItem (UIBarButtonSystemItem.Add);

			this.NavigationItem.RightBarButtonItem = _addBtn;
			_addBtn.Clicked += (object sender, EventArgs e) => {
				Client c = null;
				this.NavigationController.PushViewController(new CoconutClientAddViewController(c){Pushing = true},true);
			};


			Root = new RootElement ("Coconut Client List") {
			};
			//var Section = new Section ();


			var index = '0';
			var s = new Section ();
			foreach( var client in nameList){
				if (index == client.lastName [0]) {
					s.Add (createNameStringElement(client));
				} else {
					index = client.lastName [0];
					s = new Section (client.lastName[0].ToString().ToUpper());
					s.Add (createNameStringElement(client));
					Root.Add (s);
				}
			}

		}

		public List<Client> sortNamesbyAlphebica ()
		{
			HttpClient.ClientList.Sort ();
			return HttpClient.ClientList;
		}

		public StringElement createNameStringElement(Client c){
			return new StringElement (c.firstName + " " + c.lastName, ()=>{
				UIActionSheet actionSheet = new UIActionSheet ("Option",null,"Cancel","Call  "+c.firstName,"Edit");
				actionSheet.ShowInView(this.View);
				actionSheet.Clicked += delegate(object a, UIButtonEventArgs b){
					if(b.ButtonIndex == 0){
						Console.Out.WriteLine("Call");
					}else if(b.ButtonIndex == 1){
						Console.Out.WriteLine("Edit");
						this.NavigationController.PushViewController(new CoconutClientAddViewController(c){Pushing =true},true);

					}else if (b.ButtonIndex == 2){
						Console.Out.WriteLine("Cancel");

					}
				};
			});
		}


	}


}
