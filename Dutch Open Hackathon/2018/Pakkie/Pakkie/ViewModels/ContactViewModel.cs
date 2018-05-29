using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        public ImageSource Avatar
        {
            get => get<ImageSource>(nameof(Avatar));
            set => set(nameof(Avatar), value);
        }

        public string Name
        {
            get => get<string>(nameof(Name));
            set => set(nameof(Name), value);
        }

        public string Address
        {
            get => get<string>(nameof(Address));
            set => set(nameof(Address), value);
        }

        public bool IsSelected
        {
            get => get<bool>(nameof(IsSelected));
            set => set(nameof(IsSelected), value);
        }
        
    }
}