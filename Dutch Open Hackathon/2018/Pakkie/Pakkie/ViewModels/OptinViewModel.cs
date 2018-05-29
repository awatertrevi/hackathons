using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class OptinViewModel : ViewModelBase
    {
        public ICommand NextCommand
        {
            get => get<ICommand>(nameof(NextCommand));
            set => set(nameof(NextCommand), value);
        }

        public ObservableCollection<PartnerViewModel> Partners
        {
            get => get<ObservableCollection<PartnerViewModel>>(nameof(Partners));
            set => set(nameof(Partners), value);
        }

        public OptinViewModel()
        {
            Partners = new ObservableCollection<PartnerViewModel>
            {
                new PartnerViewModel
                {
                    Name = "PostNL",
                    Logo = new FileImageSource { File ="postnl.png"},
                    OptInMessage = "Pakkie heeft toestemming om bij al je pakket informatie te komen",
                    HasOptIn = false
                },
                new PartnerViewModel
                {
                    Name = "DHL",
                    Logo = new FileImageSource { File ="dhl.png"},
                    OptInMessage = "Pakkie heeft toestemming om bij al je pakket informatie te komen",
                    HasOptIn = false
                }
            };
        }
    }
}