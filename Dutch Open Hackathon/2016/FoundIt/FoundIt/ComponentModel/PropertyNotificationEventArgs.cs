//
// PropertyNotificationEventArgs.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using Xamarin.Forms;

namespace FoundIt.ComponentModel
{
    public class PropertyNotificationEventArgs : PropertyChangingEventArgs
    {
        public PropertyNotificationEventArgs(string propertyName, object oldValue = null, object newValue = null) : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public bool Cancel { get; set; }

        public object NewValue { get; }
        public object OldValue { get; }
    }
}
