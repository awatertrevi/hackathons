using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pakkie.Framework;
using Pakkie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptinPage : PageBase<OptinViewModel>
    {
        public OptinPage()
        {
            ViewModel = new OptinViewModel
            {
                NextCommand = new Command(async () => await Navigation.PushAsync(new IcpPage()))
            };
            BindingContext = ViewModel;
            InitializeComponent();
        }

        private void ListView_NoItemSelection(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}