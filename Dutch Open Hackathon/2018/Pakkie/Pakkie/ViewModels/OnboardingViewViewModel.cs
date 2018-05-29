using Pakkie.Framework;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class OnboardingViewViewModel : ViewModelBase
    {
        public string Title
        {
            get => get<string>(nameof(Title));
            set => set(nameof(Title), value);
        }

        public ImageSource Image
        {
            get => get<ImageSource>(nameof(Image));
            set => set(nameof(Title), value);
        }

        public string Description
        {
            get => get<string>(nameof(Description));
            set => set(nameof(Description), value);
        }
    }
}