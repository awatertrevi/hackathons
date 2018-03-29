using System;
using System.Collections.Generic;

namespace DOH2015.ComponentModel
{
    public abstract class ViewModelBase : ObservableBase
    {
#if DEBUG
        public static List<string> _collectedTypes = new List<string>();
        public static List<string> _instancedTypes = new List<string>();

        public ViewModelBase()
        {
            var xx = this.GetType().Name;
            _instancedTypes.Add(xx);
        }

        ~ViewModelBase()
        {
            var xx = this.GetType().Name;
            _collectedTypes.Add(xx);

            Console.WriteLine("Collected ViewModelBase: " + this.GetType().Name);
        }
#endif

        public bool IsBusy { get { return get(() => this.IsBusy); } set { set(() => this.IsBusy, value); } }
    }
}