using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using ElementPack;

namespace CoconutCalendarAdmin
{
	public partial class CoconutCalendarAbsebse : DialogViewController
	{
		StringElement _title;
		DateTimeElement _dateStart;
		DateTimeElement _dateEnd;
		DateTime _dateStartSelected;
		SimpleMultilineEntryElement _notes;
		CoconutLocationRadioGroup _locationRadioGroup;
		RadioGroup _staffRadioGroup;
		int _locationSelected = -1;
		Section _staffNames;
		public CoconutCalendarAbsebse (string type) : base (UITableViewStyle.Grouped, null)
		{

			var saveBtn = new UIBarButtonItem (UIBarButtonSystemItem.Save);
			this.NavigationItem.RightBarButtonItem = saveBtn;
			saveBtn.Clicked += (object sender, EventArgs e) => {
				new UIAlertView ("Successful","Schedule Saved", null, "Ok", null).Show ();
				this.NavigationController.PopToRootViewController(true);
			};

			this.Pushing = true;
			Root = new RootElement (type);
			var section = new Section ();


			_title = new StringElement ("Title", type);
			section.Add (_title);

			_locationRadioGroup = new CoconutLocationRadioGroup (0);
			_locationSelected = 0;

			_locationRadioGroup.OnSelected +=  (object sender, EventArgs e) => {
				_locationSelected = _locationRadioGroup.selectL;
				//Console.Out.WriteLine(_serviceNames.ToString());

//				while(_staffNames.Count >0){
//					_staffNames.Remove(0);
//				}
//				foreach (var i in HttpClient.LocationList[_locationSelected].LocationService) {
//
//					_staffNames.Add (new RadioElement(i.name));
//				}
//				_staffRadioGroup.Selected=0;

				while(_staffNames.Count > 0){
					_staffNames.Remove(0);
				}
//				for (int i=0; i< _staffNames.Count; i++){
//					_staffNames.Remove(i);
//				}
				_staffRadioGroup.Selected=0;

				foreach (var i in HttpClient.LocationList[_locationSelected].locationStaff) {
					_staffNames.Add (new RadioElement(i.first_name+", "+i.last_name));
				}

//				(serviceS.Elements [0] as RootElement).Reload (serviceNames,UITableViewRowAnimation.Fade);
//				this.Root.Reload(GlobalS,UITableViewRowAnimation.Fade);
			};


			var locationRoot = new RootElement ("Location", _locationRadioGroup);
			var locationNameS = new Section ();

			foreach (var i in HttpClient.LocationList){
				locationNameS.Add (new RadioElement(i.name));
			}
			locationRoot.Add (locationNameS);

			section.Add (locationRoot);

			_staffRadioGroup = new RadioGroup (0);

			var staffRoot = new RootElement ("Staff", _staffRadioGroup);
			_staffNames = new Section ();

			if(_locationSelected != -1){
				foreach (var i in HttpClient.LocationList[_locationSelected].LocationService) {
					_staffNames.Add (new RadioElement(i.name));
				}
			}
			staffRoot.Add (_staffNames);
			section.Add (staffRoot);


			_dateStart = new DateTimeElement ("Date Start",DateTime.Now);
			_dateStart.BackgroundColor = UIColor.White;
			_dateStart.DateSelected +=   (DateTimeElement obj) => {
				_dateStartSelected = obj.DateValue;
				_dateEnd.FormatDate(_dateStartSelected);
				_dateEnd.DateValue = _dateStartSelected.AddDays(1f);
				this.Root.Reload(section,UITableViewRowAnimation.Fade);

			};
			//dateStartS.Add (_dateStart);
			section.Add (_dateStart);

			_dateEnd = new DateTimeElement ("Date End",DateTime.Now);
			_dateEnd.BackgroundColor = UIColor.White;
			section.Add (_dateEnd);
			Root.Add (section);

			var noteS = new Section ("Note");
			_notes = new SimpleMultilineEntryElement (" ", " ") {
				Editable = true,
			};
			_notes.ShouldReturn += () => {
				this.View.EndEditing(true);
				return true;
			};


			noteS.Add (_notes);
			Root.Add(noteS);
		}
	}
}
