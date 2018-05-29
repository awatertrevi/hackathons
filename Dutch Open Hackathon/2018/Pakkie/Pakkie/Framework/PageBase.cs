using Xamarin.Forms;

namespace Pakkie.Framework
{
    public abstract class PageBase<T> : ContentPage where T: ViewModelBase
    {
        protected T ViewModel { get; set; }

        protected PageBase()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}