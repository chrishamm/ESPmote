using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESPmote.View
{
    public class FernbedienungsseiteViewModel : ViewModelBase
    {
        public FernbedienungsseiteViewModel(bool isEmpty)
        {
            IsLearning = isEmpty;
        }

        private bool isLearning;
        public bool IsLearning
        {
            get => isLearning;
            set
            {
                SetProperty(ref isLearning, value);
                OnPropertyChanged("ModeCaption");
            }
        }

        public string ModeCaption
        {
            get => (IsLearning) ? "Einlesemodus" : "Sendemodus";
        }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public  partial class Fernbedienungsseite : ContentPage
	{
        private FernbedienungsseiteViewModel vm;
        private Model.Fernbedienung fernbedienung;

		public Fernbedienungsseite(Model.Fernbedienung f)
		{
			InitializeComponent();

            vm = new FernbedienungsseiteViewModel(!App.Datenbank.GetFernbedienungButtons(f).Any());
            BindingContext = vm;

            Title = f.Name;
            fernbedienung = f;
        }

        protected override void OnAppearing()
        {
            foreach(Xamarin.Forms.View obj in grdContent.Children)
            {
                if (obj is Button)
                {
                    Button btn = (Button)obj;
                    Model.FernbedienungButton fbButton = App.Datenbank.GetFernbedienungButton(fernbedienung, Grid.GetRow(btn), Grid.GetColumn(btn));

                    btn.Text = (fbButton == null) ? "" : fbButton.Beschriftung;
                    btn.BackgroundColor = (fbButton == null) ? Color.Transparent : Color.Blue;
                }
            }
        }

        public async void ButtonPressed(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            int zeile = Grid.GetRow(senderButton);
            int spalte = Grid.GetColumn(senderButton);
            if (vm.IsLearning)
            {
                await Navigation.PushAsync(new ButtonZuweisen(fernbedienung, zeile, spalte));
            }
            else
            {
                Model.FernbedienungButton fbButton = App.Datenbank.GetFernbedienungButton(fernbedienung, zeile, spalte);
                if (fbButton != null)
                {
                    ESPClient.Send(fbButton.IRCode);
                }
            }
        }

        public void ModusAendern(object sender, EventArgs e)
        {
            vm.IsLearning = !vm.IsLearning;
        }

        public async void FernbedienungLoeschen(object sender, EventArgs e)
        {
            App.Datenbank.DeleteFernbedienung(fernbedienung);
            await Navigation.PopAsync();
        }
	}
}