using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FoundIt.Views.Cells;
using MonkeyChat;

namespace FoundIt.Views.Cells
{
    class MyDataTemplateSelector : DataTemplateSelector
    {
        public MyDataTemplateSelector()
        {
            // Retain instances!
            this.incomingDataTemplate = new DataTemplate(typeof(IncommingCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;
            return messageVm.Incomming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
    }
}
