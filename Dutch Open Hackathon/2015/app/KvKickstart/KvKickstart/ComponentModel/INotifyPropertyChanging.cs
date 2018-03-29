using System;
using System.ComponentModel;

namespace DOH2015.ComponentModel
{
    public interface INotifyPropertyChanging
    {
        // Summary:
        //     Occurs when a property value is changing.
        event PropertyChangingEventHandler PropertyChanging;
    }
}