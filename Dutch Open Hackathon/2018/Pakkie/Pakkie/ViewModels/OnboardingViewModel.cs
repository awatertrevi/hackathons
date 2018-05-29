using System.Collections.ObjectModel;
using System.Windows.Input;
using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class OnboardingViewModel : ViewModelBase
    {
        public ObservableCollection<OnboardingViewViewModel> OnboardingViews
        {
            get => get<ObservableCollection<OnboardingViewViewModel>>(nameof(OnboardingViews));
            set => set(nameof(OnboardingViews), value);
        }

        public ICommand SwipeLeftCommand
        {
            get => get<ICommand>(nameof(SwipeLeftCommand));
            set => set(nameof(SwipeLeftCommand), value);
        }

        public ICommand SwipeRightCommand
        {
            get => get<ICommand>(nameof(SwipeRightCommand));
            set => set(nameof(SwipeRightCommand), value);
        }
    }
}