using System.Collections.ObjectModel;
using System.Windows.Input;
using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class IcpViewModel : ViewModelBase
    {
        public ObservableCollection<ContactViewModel> Contacts
        {
            get => get<ObservableCollection<ContactViewModel>>(nameof(Contacts));
            set => set(nameof(Contacts), value);
        }

        public ICommand NextCommand
        {
            get => get<ICommand>(nameof(NextCommand));
            set => set(nameof(NextCommand), value);
        }

        public IcpViewModel()
        {
            Contacts = new ObservableCollection<ContactViewModel>
            {
                new ContactViewModel
                {
                    Name = "Peter Pan",
                    Address = "Keizerlierstraat 12, 6552AL",
                    Avatar = new FileImageSource { File = "user1.png"}
                },
                new ContactViewModel
                {
                    Name = "Mama",
                    Address = "Kinkonglaan 2, 6552KK",
                    Avatar = new FileImageSource { File = "user4.png"}
                },
                new ContactViewModel
                {
                    Name = "Oma",
                    Address = "Leidsedwarsstraat 12, 5142JD",
                    Avatar = new FileImageSource { File = "user3.png"}
                },
                new ContactViewModel
                {
                    Name = "Pieter Post",
                    Address = "Keizerlierstraat 31, 6552AL",
                    Avatar = new FileImageSource { File = "user2.png"}
                },
                new ContactViewModel
                {
                    Name = "Wanda Wonder",
                    Address = "Keizerlierstraat 59, 6552AL",
                    Avatar = new FileImageSource { File = "user5.png"}
                }
            };
        }
    }
}