using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ESPmote.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Einrichtungsseite : ContentPage
    {
        EinrichtungsseiteViewModel vm = new EinrichtungsseiteViewModel();
        bool firstShown = true;

        public Einrichtungsseite()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            if (firstShown && Application.Current.Properties.ContainsKey("ip"))
            {
                vm.IP = Application.Current.Properties["ip"].ToString();
                await Connect();
            }
            firstShown = false;
        }


        public async void ConnectClicked(object sender, EventArgs ea)
        {
            await Connect();
        }

        private async Task Connect()
        {
            vm.IsConnecting = true;
            if (await ESPClient.Connect(vm.IP))
            {
                // Verbindung erfolgreich hergestellt
                Application.Current.Properties["ip"] = vm.IP;
                await Application.Current.SavePropertiesAsync();            // geht laut Xamarin automatisch, aber nicht immer
                await Navigation.PushAsync(new Fernbedienungsliste());
            }
            else
            {
                // Fehler beim Verbinden
                vm.ErrorMessage = ESPClient.LastErrorMessage;
            }
            vm.IsConnecting = false;
        }
    }

    public class EinrichtungsseiteViewModel : ViewModelBase
    {
        private bool isIdle = true;
        public bool IsIdle
        {
            get => isIdle;
            set
            {
                SetProperty(ref isIdle, value);
                OnPropertyChanged("IsConnecting");
            }
        }

        public bool IsConnecting
        {
            get => !IsIdle;
            set => IsIdle = !value;
        }

        private string ip;
        public string IP
        {
            get => ip;
            set => SetProperty(ref ip, value);
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
    }
}
