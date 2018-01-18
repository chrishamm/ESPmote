using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESPmote.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Fernbedienungsliste : ContentPage
	{
		public Fernbedienungsliste()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            lstFernbedienungen.ItemsSource = App.Datenbank.GetFernbedienungen();
        }

        public async Task lstFernbedienungenItemTapped(object sender, ItemTappedEventArgs args)
        {
            Model.Fernbedienung f = (Model.Fernbedienung)args.Item;
            await Navigation.PushAsync(new Fernbedienungsseite(f));
            lstFernbedienungen.SelectedItem = null;
        }

        public async Task HinzufuegenClicked()
        {
            string name = await Helper.InputBox(Navigation, "Fernbedienung hinzufügen", "Bitte geben Sie einen Namen für die neue Fernbedienung an:");
            if (name == null)
            {
                return;
            }

            var duplikate = from f in App.Datenbank.GetFernbedienungen()
                            where f.Name.ToLowerInvariant() == name.ToLowerInvariant()
                            select f;
            if (duplikate.Any())
            {
                // Doppelte Fernbedienungen sind unzulässig
                await DisplayAlert("Fehler", "Es existiert bereits eine Fernbedienung mit dem angegebenen Namen!", "OK");
            }
            else
            {
                // Neue Fernbedienung anlegen
                App.Datenbank.SaveFernbedienung(new Model.Fernbedienung() { Name = name });
                lstFernbedienungen.ItemsSource = App.Datenbank.GetFernbedienungen();
            }
        }
    }
}