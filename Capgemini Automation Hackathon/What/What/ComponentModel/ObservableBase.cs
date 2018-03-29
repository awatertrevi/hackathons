//
// ObservableBase.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace What.ComponentModel
{
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        public delegate void PropertyNotificationEventHandler(object sender, PropertyNotificationEventArgs args);

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyNotificationEventHandler PropertyNotification;

        protected ObservableBase()
        {
            InternalState = new Dictionary<string, object>();
        }

        private readonly IDictionary<string, object> InternalState;

        protected T get<T>(Expression<Func<T>> propertyExpression)
        {
            return this.get<T>(((MemberExpression)propertyExpression.Body).Member.Name);
        }

        protected virtual T get<T>(string key)
        {
            if (!InternalState.ContainsKey(key))
                InternalState.Add(key, default(T));
            return (T)InternalState[key];
        }

        protected void set<T>(Expression<Func<T>> propertyExpression, T value)
        {
            this.set<T>(((MemberExpression)propertyExpression.Body).Member.Name, value);
        }

        protected virtual void set<T>(string key, T value)
        {
            var oldValue = get<T>(key);

            if (!AreEqual(value, oldValue))
            {
                if (onPropertyChanging(key, oldValue, value))
                {
                    InternalState[key] = value;
                    onPropertyChanged(key);
                }
            }
        }

        protected virtual void forceSet<T>(string key, T value)
        {
            var oldValue = get<T>(key);

            if (onPropertyChanging(key, oldValue, value))
            {
                InternalState[key] = value;
                onPropertyChanged(key);
            }
        }

        private static bool AreEqual<T>(T left, T right)
        {
            if (object.ReferenceEquals(left, null))
                return object.ReferenceEquals(right, null);

            return left.Equals(right);
        }

        protected bool onPropertyChanging<T>(string propertyName, T oldValue, T newValue)
        {
            var notification = new PropertyNotificationEventArgs(propertyName, oldValue, newValue);

            PropertyNotification?.Invoke(this, notification);

            return !notification.Cancel;
        }

        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void onPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.onPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }
    }
}
