﻿using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Eventador.APP.V2.Common.Helpers
{
    public class Bindable : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected bool CallPropertyChangeEvent { get; set; } = true;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected T Get<T>(T defValue = default(T), [CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name)) return defValue;
            if (_properties.TryGetValue(name, out var value))
                return (T)value;
            _properties.AddOrUpdate(name, defValue, (s, o) => defValue);
            return defValue;
        }

        protected bool Set(object value, [CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var isExists = _properties.TryGetValue(name, out var getValue);
            if (isExists && Equals(value, getValue))
                return false;

            if (CallPropertyChangeEvent)
                OnPropertyChanging(name);

            _properties.AddOrUpdate(name, value, (s, o) => value);

            if (CallPropertyChangeEvent)
                OnPropertyChanged(name);

            return true;
        }
    }
}