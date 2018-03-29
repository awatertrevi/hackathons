using System;
using What.Cells;
using What.Models;
using Xamarin.Forms;

namespace What.Utilities
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public MessageDataTemplateSelector()
        {
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
