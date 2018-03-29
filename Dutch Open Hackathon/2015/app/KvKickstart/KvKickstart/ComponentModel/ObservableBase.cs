using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DOH2015.ComponentModel
{
    public abstract class ObservableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public delegate void PropertyNotificationEventHandler(object sender, PropertyNotificationEventArgs args);

        private WeakEvent<System.ComponentModel.PropertyChangedEventHandler> weakEventPropertyChanged = new WeakEvent<System.ComponentModel.PropertyChangedEventHandler>();
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                weakEventPropertyChanged.Add(value);
            }
            remove
            {
                weakEventPropertyChanged.Remove(value);
            }
        }

        private WeakEvent<System.ComponentModel.PropertyChangingEventHandler> weakEventPropertyChanging = new WeakEvent<System.ComponentModel.PropertyChangingEventHandler>();
        public event PropertyChangingEventHandler PropertyChanging
        {
            add
            {
                weakEventPropertyChanging.Add(value);
            }
            remove
            {
                weakEventPropertyChanging.Remove(value);
            }
        }

        private WeakEvent<PropertyNotificationEventHandler> weakEventPropertyNotification = new WeakEvent<PropertyNotificationEventHandler>();
        public event PropertyNotificationEventHandler PropertyNotification
        {
            add
            {
                weakEventPropertyNotification.Add(value);
            }
            remove
            {
                weakEventPropertyNotification.Remove(value);
            }
        }

        public ObservableBase()
        {
            InternalState = new Dictionary<string, object>();
        }

        private IDictionary<string, object> InternalState;
        protected T get<T>(Expression<Func<T>> propertyExpression) { return this.get<T>(((MemberExpression)propertyExpression.Body).Member.Name); }
        virtual protected T get<T>(string key)
        {
            if (!InternalState.ContainsKey(key))
                InternalState.Add(key, default(T));
            return (T)InternalState[key];
        }

        protected void set<T>(Expression<Func<T>> propertyExpression, T value) { this.set<T>(((MemberExpression)propertyExpression.Body).Member.Name, value); }
        virtual protected void set<T>(string key, T value)
        {
            T oldValue = get<T>(key);
            if (!AreEqual(value, oldValue))
            {
                if (onPropertyChanging(key, oldValue, value))
                {
                    InternalState[key] = value;
                    onPropertyChanged(key);
                }
            }
        }

        virtual protected void forceSet<T>(string key, T value)
        {
            T oldValue = get<T>(key);

            if (onPropertyChanging(key, oldValue, value))
            {
                InternalState[key] = value;
                onPropertyChanged(key);
            }
        }

        private bool AreEqual<T>(T left, T right)
        {
            if (Object.ReferenceEquals(left, null))
                return Object.ReferenceEquals(right, null);
            return left.Equals(right);
        }

        protected bool onPropertyChanging<T>(string propertyName, T oldValue, T newValue)
        {
            var notification = new PropertyNotificationEventArgs(propertyName, oldValue, newValue);

            weakEventPropertyChanging.Invoke(this, notification);

            weakEventPropertyNotification.Invoke(this, notification);

            return !notification.Cancel;
        }

        protected void onPropertyChanged(string propertyName)
        {
            weakEventPropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        protected void onPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.onPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }
    }

    public class PropertyNotificationEventArgs : System.ComponentModel.PropertyChangingEventArgs
    {

        public PropertyNotificationEventArgs(String propertyName)
            : this(propertyName, null, null)
        {

        }

        public PropertyNotificationEventArgs(String propertyName,
            Object oldValue, Object newValue)
            : base(propertyName)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public bool Cancel { get; set; }

        private Object newValue;
        public Object NewValue
        {
            get
            {
                return this.newValue;
            }
        }

        private Object oldValue;
        public Object OldValue
        {
            get
            {
                return this.oldValue;
            }
        }

    }
}


