using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Pakkie.Framework;
using Pakkie.Services.KPN;
using Xamarin.Forms;

namespace Pakkie.ViewModels
{
    public class DeliveryViewModel : ViewModelBase
    {
        public ICommand SendMessageCommand
        {
            get => get<ICommand>(nameof(SendMessageCommand));
            set => set(nameof(SendMessageCommand), value);
        }

        public async Task Init()
        {
            var service = new SmsService();
            await service.Init();
            SendMessageCommand = new Command(async () =>
            {
                await service.SendSms("0629287356", "Hallo, Jordi heeft aangegeven dat U vandaag wel pakketje van hem wilt aannemen tussen 13:00 en 17:00. Bedankt namens Pakkie!");
            });
        }
    }
}