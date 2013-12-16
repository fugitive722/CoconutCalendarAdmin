using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using ElementPack;
using System.Drawing;

namespace CoconutCalendarAdmin
{

	public partial class CoconutScheduleAddViewController : DialogViewController
	{

		StringElement _client;
		DateTimeElement _dateStart;
		DateTimeElement _dateEnd;
		CoconutLocationRadioGroup _locationRadioGroup;
		RadioGroup _serviceRadioGroup;
		RadioGroup _staffRadioGroup;
		BooleanElement _walk;
		BooleanElement _notifyS;
		BooleanElement _notifyC;
		SimpleMultilineEntryElement _notes;
		FloatElementEx _limit;


		int _locationSelected = -1;
		DateTime _dateStartSelected;


		Boolean isClient = false;
		Section _serviceNames;
		Section _staffNames;


		public CoconutScheduleAddViewController (Boolean client) : base (UITableViewStyle.Grouped, null)
		{
			this.Pushing = true;

			if(client){
				isClient = true;
			}



			Root = new RootElement ("New");

			var GlobalS = new Section ();
			// save button


			// 1. pick up client

			_client = new StringElement ("Client", () => {
				var addClient =new CoconutCalendarScheduleAddNewClient();
				addClient.finishedSelect += (object sender, EventArgs e) => {

					if(addClient.selectedClient == null){
						_client.Value = "Click to pick ";
					}else{
						_client.Value = addClient.selectedClient.firstName +" "+ addClient.selectedClient.lastName;
						this.Root.Reload(_client,UITableViewRowAnimation.None);

					}
				};
				this.NavigationController.PushViewController(addClient,true);

			});

			_client.Value = "Click to pick ";
			GlobalS.Add (_client);





			// 2. Pick up date start / end
			//var dateStartS = new Section ();
			//var dateEndS = new Section ();

			_dateStart = new DateTimeElement ("Date Start",DateTime.Now);
			_dateStart.BackgroundColor = UIColor.White;
			_dateStart.DateSelected +=   (DateTimeElement obj) => {
				_dateStartSelected = obj.DateValue;
				_dateEnd.FormatDate(_dateStartSelected);
				_dateEnd.DateValue = _dateStartSelected.AddMinutes(15);
				this.Root.Reload(GlobalS,UITableViewRowAnimation.Fade);

			};
			//dateStartS.Add (_dateStart);
			GlobalS.Add (_dateStart);

			_dateEnd = new DateTimeElement ("Date End",DateTime.Now);
			_dateEnd.BackgroundColor = UIColor.White;
			GlobalS.Add (_dateEnd);




			// 3. Location, Service, Staff
			//var locationS = new Section ();
			//var serviceS = new Section ();


			//var staffS = new Section ();




			//serviceS.Add (serviceRoot);



			//staffS.Add (staffRoot);


			_locationRadioGroup = new CoconutLocationRadioGroup (0);
			_locationSelected = 0;

			_locationRadioGroup.OnSelected +=  (object sender, EventArgs e) => {
				_locationSelected = _locationRadioGroup.selectL;
				//Console.Out.WriteLine(_serviceNames.ToString());

				while(_serviceNames.Count >0){
					_serviceNames.Remove(0);
				}
				foreach (var i in HttpClient.LocationList[_locationSelected].LocationService) {

					_serviceNames.Add (new RadioElement(i.name));
				}
				_serviceRadioGroup.Selected=0;

				while(_staffNames.Count > 0){
					_staffNames.Remove(0);
				}
				for (int i=0; i< _staffNames.Count; i++){
					_staffNames.Remove(i);
				}
				_staffRadioGroup.Selected=0;

				foreach (var i in HttpClient.LocationList[_locationSelected].locationStaff) {
					_staffNames.Add (new RadioElement(i.first_name+", "+i.last_name));
				}

				//(serviceS.Elements [0] as RootElement).Reload (serviceNames,UITableViewRowAnimation.Fade);
//				this.Root.Reload(GlobalS,UITableViewRowAnimation.Fade);
			};

			var locationRoot = new RootElement ("Location", _locationRadioGroup);
			var locationNameS = new Section ();

			foreach (var i in HttpClient.LocationList){
				locationNameS.Add (new RadioElement(i.name));
			}


			_serviceRadioGroup = new RadioGroup (0);

			var serviceRoot = new RootElement ("Service", _serviceRadioGroup);
			_serviceNames = new Section ();
			if(_locationSelected != -1){
				foreach (var i in HttpClient.LocationList[_locationSelected].LocationService) {
					_serviceNames.Add (new RadioElement(i.name));
				}
			}
			serviceRoot.Add (_serviceNames);

			_staffRadioGroup = new RadioGroup (0);

			var staffRoot = new RootElement ("Staff", _staffRadioGroup);
			_staffNames = new Section ();
			if (_locationSelected != -1){
				foreach(var i in HttpClient.LocationList[_locationSelected].locationStaff){
					_staffNames.Add (new RadioElement(i.first_name+", "+i.last_name));
				}
			}
			staffRoot.Add (_staffNames);



			locationRoot.Add (locationNameS);
			//locationS.Add (locationRoot);
			GlobalS.Add (locationRoot);
			GlobalS.Add (serviceRoot);
			GlobalS.Add (staffRoot);
			Root.Add (GlobalS);


			if (! isClient) {
				var limitS = new Section ("Limit Client");

				var unlimit = new BooleanElement ("Unlimit Client",false);
				unlimit.ValueChanged += (object sender, EventArgs e) => {
					if(unlimit.Value){
						limitS.Remove(1);

					}else{
						_limit = new FloatElementEx (0,null,true,false){
							//Caption = "Hello world",
							UseCaptionForValueDisplay = true,
						};
						limitS.Add (_limit);
					}

					this.Root.Reload(limitS,UITableViewRowAnimation.Fade);
				};

				_limit = new FloatElementEx (0,null,true,false){
					//Caption = "Hello world",
					UseCaptionForValueDisplay = true,
				};

//				_limit.OnSelected +=  (object sender, EventArgs e) => {
//					limitS.Header = _limit.Value.ToString();
//					this.Root.Reload(limitS,UITableViewRowAnimation.Fade);
//				};
				limitS.Add (unlimit);
				limitS.Add (_limit);
				Root.Add(limitS);

			}

			//GlobalS.Add (_limit);

			_walk = new BooleanElement ("Walk in",false);
			GlobalS.Add (_walk);
			//var walkS = new Section ();
			//walkS.Add (_walk);

			_notifyC = new BooleanElement ("Notify Client", false);
			GlobalS.Add(_notifyC);
			//var notifyC = new Section ();
			//notifyC.Add (_notifyC);

			_notifyS = new BooleanElement ("Notify Staff", false);
			GlobalS.Add (_notifyS);
			//var notifyS = new Section ();
			//notifyS.Add (_notifyS);




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



//			Root.Add (clientS);
//			Root.Add (dateStartS);
//			Root.Add (dateEndS);
//			Root.Add (locationS);
//			Root.Add (serviceS);
//			Root.Add (staffS);
//			Root.Add (walkS);
//			Root.Add (notifyC);
//			Root.Add (notifyS);





			var saveBtn = new UIBarButtonItem (UIBarButtonSystemItem.Save);
			saveBtn.Clicked += (object sender, EventArgs e) => {
				_notes.ResignFirstResponder(true);
				new UIAlertView ("Saved", createScheduleJson(), null, "Ok", null).Show ();
			};
			this.NavigationItem.RightBarButtonItem = saveBtn;

//			var tap = new UITapGestureRecognizer ();
//			tap.AddTarget (() =>{
//				this.View.EndEditing (true);
//			});
//			this.View.AddGestureRecognizer (tap);

		}

		public string createScheduleJson(){
			string s = string.Format ("Client : {0} \n DateStart : {1} \n DateEnd : {2} \n " +
			                          "Location: {3} \n Service: {4}\n Staff: {5}\n" +
			                          "Walkin : {6} \n Notify Satff : {7} \n Notify Client: {8}\n" +
			                          "Note: {9}\n",
				//HttpClient.ClientList[_clientRadioGroup.Selected].firstName,
			                          _dateStart.Value.ToString(),
			                          _dateEnd.Value.ToString(),
			                          HttpClient.LocationList[_locationRadioGroup.Selected].name,
			                          HttpClient.LocationList[_locationRadioGroup.Selected].LocationService[_serviceRadioGroup.Selected].name,
			                          HttpClient.LocationList[_locationRadioGroup.Selected].locationStaff[_staffRadioGroup.Selected].first_name,
			                          _walk.Value.ToString(),
			                          _notifyS.Value.ToString(),
			                          _notifyC.Value.ToString(),
			                          _notes.Value);
			return s;
		}

		public void initializeInput (Appointment appointment){

			// client name
			var client = appointment.client [0].id;

			for ( int i=0; i<HttpClient.ClientList.Count; i++){

				if (client == HttpClient.ClientList[i].id) {
					//_clientRadioGroup.Selected = i;
				}

			}

			// start time
			var startTime = appointment.start.Split ('-');
			var startYear = startTime [0];
			var startMonth = startTime [1];
			var startTime1 = startTime [2].Split (' ');
			var startDay = startTime1 [0];
			var startTime3 = startTime1 [1].Split (':');
			var startHour = startTime3 [0];
			var startMin = startTime3 [1];

			var sd = new DateTime (Convert.ToInt32(startYear),Convert.ToInt32(startMonth),Convert.ToInt32(startDay),
			                      Convert.ToInt32(startHour),Convert.ToInt32(startMin),00);
			_dateStart.DateValue = sd;


			// end time
			var endTime = appointment.end.Split ('-');
			var endYear = endTime [0];
			var endMonth = endTime[1];
			var endTime1 = endTime [2].Split (' ');
			var endDay = endTime1 [0];
			var endTime3 = endTime1 [1].Split (':');
			var endHour = endTime3 [0];
			var endMin = endTime3 [1];

			var ed = new DateTime (Convert.ToInt32(endYear),Convert.ToInt32(endMonth),Convert.ToInt32(endDay),
			                       Convert.ToInt32(endHour),Convert.ToInt32(endMin),00);
			_dateEnd.DateValue = ed;


			// location
			var location = appointment.location.id;

			for(int i=0; i<HttpClient.LocationList.Count; i++){
				if(location == HttpClient.LocationList[i].id){
					_locationRadioGroup.Selected = i;
				}
			}

			// service
			var service = appointment.service.id;
			for(int i=0; i<HttpClient.LocationList[_locationRadioGroup.Selected].LocationService.Count;i++){
				if(service == HttpClient.LocationList[_locationRadioGroup.Selected].LocationService[i].id){
					_serviceRadioGroup.Selected = i;
				}

			}

			// staff
			var staff = appointment.staff.id;
			for(int i=0; i<HttpClient.LocationList[_locationRadioGroup.Selected].locationStaff.Count; i++){
				if(staff == HttpClient.LocationList[_locationRadioGroup.Selected].locationStaff[i].id){
					_staffRadioGroup.Selected = i;

				}
			}

			//Notes
			_notes.Value = appointment.note;
		}


	}

	class CoconutLocationRadioGroup : RadioGroup {

		public int selectL;
		public CoconutLocationRadioGroup (int i): base(i){
		}

		public override int Selected {
			get {

				return base.Selected;
			}
			set {
				selectL = value;
				base.Selected = value;

				var selected = OnSelected;
				if(selected !=null){
					selected (this, EventArgs.Empty);
				}


			}
		}

		public event EventHandler<EventArgs> OnSelected;
	}

	class CoconutSlider : FloatElement{

		public CoconutSlider(float f) : base(null,null,f){
			this.MinValue = 0f;
			this.MaxValue = 100f;
			this.Value = f;
			this.Caption = "Hello World";
		}

	

		public override string ToString ()
		{
			return string.Format ("[CoconutSlider]");

		}
		public event EventHandler<EventArgs> OnSelected;
	}

	public class FloatElementEx : Element
	{
		static NSString skey = new NSString("FloatElementEx");
		const float LockImageWidth = 32.0f;
		const float LockImageHeight = 32.0f;

		/// <summary>
		/// Set a string to reserve a certain amount of space for the 
		/// caption used in the FloatElement. Useful when there is no
		/// initial caption to show - allows space to be reserved for 
		/// when it will be set.
		/// </summary>
		public string ReserveCaptionPlaceholderString { get; set; }
		/// <summary>
		/// Returns the locked status
		/// </summary>
		public bool IsLocked { get { return _valueLocked; } }
		public bool ShowCaption { get; set; }
		/// <summary>
		/// Ties the displayed caption to the value of the slider
		/// </summary>
		public bool UseCaptionForValueDisplay { get; set; }
		public bool Continuous { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }
		public int Value { get; private set; }
		public UIImage LockImage { get; set; }
		public UIImage UnlockImage { get; set; }

		private UIButton _lockImageView;
		private UISlider _slider;
		private Action<int> _valueChangedCallback;
		private bool _valueLocked;
		public bool _lockable { set; get;}


		public FloatElementEx(int value, Action<int> valueChanged = null, bool continuous = true, bool lockable = false)
			: base(null)
		{
			MinValue = 0;
			MaxValue = 100;
			Value = value;
			Continuous = continuous;
			_lockable = lockable;
			_valueChangedCallback = valueChanged;
			Caption = "0      ";
			ShowCaption = true;

 		}

		protected override NSString CellKey { get { return skey; } }

		public override UITableViewCell GetCell(UITableView tv)
		{
			var cell = tv.DequeueReusableCell(CellKey);
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Default, CellKey);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}
			else
				RemoveTag(cell, 1);

			SizeF captionSize = new SizeF(0, 0);
			if (ShowCaption && (Caption != null || ReserveCaptionPlaceholderString != null || UseCaptionForValueDisplay)) {
				if (Caption == null) {
					if (UseCaptionForValueDisplay)
						captionSize = cell.TextLabel.StringSize(MaxValue.ToString(), 
							UIFont.FromName(cell.TextLabel.Font.Name, UIFont.LabelFontSize));
					else if (!string.IsNullOrEmpty(ReserveCaptionPlaceholderString))
						captionSize = cell.TextLabel.StringSize(ReserveCaptionPlaceholderString, 
							UIFont.FromName(cell.TextLabel.Font.Name, UIFont.LabelFontSize));
				}
				else {
					captionSize = cell.TextLabel.StringSize(Caption, UIFont.FromName(cell.TextLabel.Font.Name, UIFont.LabelFontSize));
				}

				captionSize.Width += 10; // Spacing

				if (Caption != null)
					cell.TextLabel.Text = Caption;
			}

			var lockImageWidth = _lockable ? LockImageWidth : 0;

			if (_slider == null) {
				_slider = new UISlider(new RectangleF(10f + captionSize.Width, 12f, 280f - captionSize.Width - lockImageWidth, 7f)) {
					BackgroundColor = UIColor.Clear,
					MinValue = this.MinValue,
					MaxValue = this.MaxValue,
					Continuous = this.Continuous,
					Value = this.Value,
					Tag = 1
				};
				_slider.ValueChanged += delegate {
					Value = (int)_slider.Value;
					if (UseCaptionForValueDisplay) {
						Caption = Value.ToString();
						// force repaint/redraw
						if (GetContainerTableView() != null) {
							var root = GetImmediateRootElement();
							root.Reload(this, UITableViewRowAnimation.None);
						}
					}
					if (_valueChangedCallback != null)
						_valueChangedCallback(Value);
				};
			}
			else {
				_slider.Value = Value;
			}

			if (_lockable){
				if (_lockImageView == null)
					_lockImageView = new UIButton(new RectangleF(_slider.Frame.X + _slider.Frame.Width, 2f, lockImageWidth, LockImageHeight));

				_lockImageView.SetBackgroundImage((_valueLocked) ? LockImage : UnlockImage, UIControlState.Normal);
				_lockImageView.TouchUpInside += (object sender, EventArgs e) => {
					_valueLocked = !_valueLocked;
					_lockImageView.SetBackgroundImage((_valueLocked) ? LockImage : UnlockImage, UIControlState.Normal);
					if (_valueLocked)
						_slider.Enabled = (!_valueLocked);
				};
				cell.ContentView.AddSubview(_lockImageView);
			}
			cell.ContentView.AddSubview(_slider);
			return cell;
		}
		public override string Summary()
		{
			return Value.ToString();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_slider != null)
				{
					_slider.Dispose();
					_slider = null;
				}
			}
		}

		public void SetValue(int f)
		{
			if (!IsLocked)
				_slider.SetValue(f, false);
		}

		public void SetCaption(string caption)
		{
			Caption = caption;
			// force repaint/redraw
			if (GetContainerTableView() != null) {
				var root = GetImmediateRootElement();
				root.Reload(this, UITableViewRowAnimation.None);
			}
		}
	}

}








































