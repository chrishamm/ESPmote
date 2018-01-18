using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESPmote.View
{
    public class ButtonZuweisenViewModel : ViewModelBase
    {
        public ButtonZuweisenViewModel(Model.FernbedienungButton fb)
        {
            Beschriftung = fb.Beschriftung;
            EingelesenerCode = fb.IRCode;
        }

        private string beschriftung;
        public string Beschriftung
        {
            get => beschriftung;
            set
            {
                SetProperty(ref beschriftung, value);
                OnPropertyChanged("EingabenOK");
            }
        }

        private string eingelesenerCode;
        public string EingelesenerCode
        {
            get => eingelesenerCode;
            set
            {
                SetProperty(ref eingelesenerCode, value);
                OnPropertyChanged("CodeWurdeEingelesen");
                OnPropertyChanged("EinleseStatusColor");
                OnPropertyChanged("EinleseStatusText");
                OnPropertyChanged("EingabenOK");
            }
        }

        private bool codeWirdEingelesen;
        public bool CodeWirdEingelesen {
            get => codeWirdEingelesen;
            set
            {
                SetProperty(ref codeWirdEingelesen, value);
                OnPropertyChanged("CodeKannEingelesenWerden");
            }
        }
        public bool CodeKannEingelesenWerden { get => !CodeWirdEingelesen; }

        public bool CodeWurdeEingelesen { get => EingelesenerCode != null; }
        public Color EinleseStatusColor { get => (EingelesenerCode == null) ? Color.Red : Color.Green; }
        public string EinleseStatusText { get => (EingelesenerCode == null) ? "Kein Code eingelesen" : "Code eingelesen"; }

        public bool EingabenOK { get => Beschriftung.Trim() != "" && EingelesenerCode != null; }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ButtonZuweisen : ContentPage
	{
        private Model.FernbedienungButton btn;
        private ButtonZuweisenViewModel vm;

		public ButtonZuweisen(Model.Fernbedienung fb, int zeile, int spalte)
		{
			InitializeComponent();

            btn = App.Datenbank.GetFernbedienungButton(fb, zeile, spalte);
            if (btn == null)
            {
                btn = new Model.FernbedienungButton() { Fernbedienung = fb.ID, Zeile = zeile, Spalte = spalte };
            }

            vm = new ButtonZuweisenViewModel(btn);
            BindingContext = vm;
		}

        public async void CodeEinlesenClicked(object sender, EventArgs args)
        {
            vm.CodeWirdEingelesen = true;
            vm.EingelesenerCode = await ESPClient.Receive();
            vm.CodeWirdEingelesen = false;
        }

        public async void ButtonUebernehmen(object sender, EventArgs args)
        {
            btn.Beschriftung = vm.Beschriftung;
            btn.IRCode = vm.EingelesenerCode;
            App.Datenbank.SaveFernbedienungsButton(btn);

            await Navigation.PopAsync();
        }
    }
}