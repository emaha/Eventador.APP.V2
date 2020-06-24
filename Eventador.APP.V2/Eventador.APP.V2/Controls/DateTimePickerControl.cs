using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace Eventador.APP.V2.Controls
{
    public class DateTimeControl : StackLayout
    {
        private readonly DatePicker _date;
        private readonly TimePicker _time;
        public DateTimeControl() 
        {
            this.Orientation = StackOrientation.Horizontal;
            this.Padding = 2;

            _date = new DatePicker();
            _date.Format = CultureInfo.DefaultThreadCurrentUICulture.DateTimeFormat.ShortDatePattern;
            _time = new TimePicker();
            _time.Format = CultureInfo.DefaultThreadCurrentUICulture.DateTimeFormat.ShortTimePattern;
            this.Children.Add(_date);
            this.Children.Add(_time); 
            _date.PropertyChanged += DateOnPropertyChanged;
            _time.PropertyChanged += TimeOnPropertyChanged; 
        }
        private void TimeOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Time")
            {
                Value = _date.Date.Add(_time.Time);
            }
        }
        private void DateOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Date")
            {
                Value = _date.Date.Add(_time.Time);
            }
        }
        #region Value 
        public static readonly BindableProperty ValueProperty = BindableProperty.Create<DateTimeControl, DateTime>(p => p.Value, default); 
        public DateTime Value 
        { 
            get 
            { 
                return (DateTime)GetValue(ValueProperty); 
            } 
            set 
            { 
                SetValue(ValueProperty, value); 
            } 
        }
        #endregion }
    }
}
