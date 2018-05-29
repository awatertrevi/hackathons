using System;
using System.Collections.Generic;
using System.Text;
using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class PartnerViewModel : ViewModelBase
    {
        public ImageSource Logo
        {
            get => get<ImageSource>(nameof(Logo));
            set => set(nameof(Logo), value);
        }

        public string Name
        {
            get => get<string>(nameof(Name));
            set => set(nameof(Name), value);
        }

        public string OptInMessage
        {
            get => get<string>(nameof(OptInMessage));
            set => set(nameof(OptInMessage), value);
        }

        public bool HasOptIn
        {
            get => get<bool>(nameof(HasOptIn));
            set => set(nameof(HasOptIn), value);
        }
    }
}
