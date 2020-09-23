using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Eventador.APP.V2.Controls
{
    public class DateTimeControl : ContentView, INotifyPropertyChanged
    {
        public Entry _entry { get; private set; } = new Entry();
        public DatePicker _datePicker { get; private set; } = new DatePicker() { MinimumDate = DateTime.Today, IsVisible = false };
        public TimePicker _timePicker { get; private set; } = new TimePicker() { IsVisible = false };
        private string _stringFormat { get; set; }
        public string StringFormat { get { return _stringFormat ?? "dd/MM/yyyy HH:mm"; } set { _stringFormat = value; } }

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set
            {
                SetValue(DateTimeProperty, value);
                OnPropertyChanged(nameof(DateTime));
            }
        }

        private TimeSpan _time
        {
            get { return TimeSpan.FromTicks(DateTime.Ticks); }
            set { DateTime = new DateTime(DateTime.Date.Ticks).AddTicks(value.Ticks); }
        }

        private DateTime _date
        {
            get { return DateTime.Date; }
            set { DateTime = new DateTime(DateTime.TimeOfDay.Ticks).AddTicks(value.Ticks); }
        }

        public static readonly BindableProperty DateTimeProperty =
            BindableProperty.Create(
            nameof(DateTime),
            typeof(DateTime),
            typeof(DateTimeControl),
            DateTime.Now,
            BindingMode.TwoWay,
            propertyChanged: DTPropertyChanged);


        public DateTimeControl()
        {
            BindingContext = this;

            Content = new StackLayout()
            {
                Children =
                {
                    _datePicker,
                    _timePicker,
                    _entry
                }
            };


            _datePicker.SetBinding(DatePicker.DateProperty, nameof(_date));
            _timePicker.SetBinding(TimePicker.TimeProperty, nameof(_time));
            _timePicker.Unfocused += (sender, args) => _time = _timePicker.Time;
            _datePicker.Focused += (s, a) => UpdateEntryText();

            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => _datePicker.Focus())
            });
            _entry.Focused += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() => _datePicker.Focus());
            };
            _datePicker.Unfocused += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _timePicker.Focus();
                    _date = _datePicker.Date;
                    UpdateEntryText();
                });
            };
        }

        private void UpdateEntryText()
        {
            _entry.Text = DateTime.ToString(StringFormat);
        }

        private static void DTPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var timePicker = bindable as DateTimeControl;
            timePicker.UpdateEntryText();
        }
    }
}