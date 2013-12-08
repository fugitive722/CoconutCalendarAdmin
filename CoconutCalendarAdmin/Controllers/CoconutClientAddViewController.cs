using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using ElementPack;

namespace CoconutCalendarAdmin
{
	public partial class CoconutClientAddViewController : DialogViewController
	{

		public event EventHandler ClientCreated;

		EntryElement _firstName = new EntryElement("First Name"," ","");
		EntryElement _lastName = new EntryElement ("Last Name"," ","");
		EntryElement _mail = new EntryElement ("Mail"," ","");
		EntryElement _address = new EntryElement ("address"," ","");
		EntryElement _zip = new EntryElement("Zip / PostCode"," ","");
		EntryElement _phone = new EntryElement ("Phone"," ",""); 
		EntryElement _cellPhone = new EntryElement ("Cell Phone"," ",""); 
		EntryElement _workPhone = new EntryElement("Work Phone"," ","");
		SimpleMultilineEntryElement _notes = new SimpleMultilineEntryElement ("",""){
			Editable = true,
		};
		public CoconutClientAddViewController (Client c) : base (UITableViewStyle.Grouped, null)
		{
			var _saveBtn = new UIBarButtonItem (UIBarButtonSystemItem.Save);
			this.NavigationItem.RightBarButtonItem = _saveBtn;
			_saveBtn.Clicked += (object sender, EventArgs e) => {
				new UIAlertView("Saved","save data in the server",null,"OK",null).Show();

				var client = new Client ();
				client.firstName = _firstName.Value;
				client.lastName = _lastName.Value;
				client.email = _mail.Value;
				client.address = _address.Value;
				client.zip = _zip.Value;
				client.phone = _phone.Value;
				client.cell_phone = _cellPhone.Value;
				client.work_phone = _workPhone.Value;
				client.notes = _notes.Value;

				HttpClient.ClientList.Insert(0,client);

				if(ClientCreated != null){
					ClientCreated(this,EventArgs.Empty);
				}

				this.NavigationController.PopViewControllerAnimated(true);
			};

			Root = new RootElement ("Add New Client") {
			};

			if(c != null){
				setClientInformation (c);
				//Root.TableView.ReloadData ();
			}

			var Section = new Section ();
			Section.Add (_firstName);
			Section.Add (_lastName);
			Section.Add (_mail);
			Section.Add (_address);
			Section.Add (_zip);
			Section.Add (_phone);
			Section.Add (_cellPhone);
			Section.Add (_workPhone);


			Root.Add (Section);

			var noteS = new Section ("Notes");
			noteS.Add (_notes);
			Root.Add (noteS);


			var tap = new UITapGestureRecognizer ();
			tap.AddTarget (() =>{
				this.View.EndEditing (true);
			});
			this.View.AddGestureRecognizer (tap);


		}

		public void setClientInformation(Client c){
			_firstName.Value = c.firstName;
			_lastName.Value = c.lastName;
			_mail.Value = c.email;
			_address.Value = c.address;
			_zip.Value = c.zip;
			_phone.Value = c.phone;
			_cellPhone.Value = c.cell_phone;
			_workPhone.Value = c.work_phone;
			_notes.Value = c.notes;
		}
	}


	
}
