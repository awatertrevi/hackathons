using ATXF.ComponentModel;

namespace Pakkie.Framework
{
    public class ViewModelBase : ObservableBase
    {

        public string Title
        {
            get => get<string>(nameof(Title));
            set => set(nameof(Title), value);
        }
    }
}