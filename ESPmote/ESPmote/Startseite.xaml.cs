using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ESPmote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Startseite : ContentPage
    {
        public Startseite()
        {
            InitializeComponent();
            button_list(dummy_list());
        }

        public void button_list(List<string> remotelist) // erzeugt aus der fernbedienungsliste buttons und fügt diese ein
        {
            var command_fernbedienungsseite = new Command(clicked_fernbedienungsseite);
            var command_einlernseite = new Command(clicked_einlernseite);

            StackLayout layout = new StackLayout();

            foreach (string elem in remotelist)
            {
                layout.Children.Add(new Button { Text = elem, HorizontalOptions = LayoutOptions.Start, Command = command_fernbedienungsseite });
            }

            layout.Children.Add(new Button { Text = "Einlernen", HorizontalOptions = LayoutOptions.Start, Command = command_einlernseite });

            Content = layout;
        }


        public List<string> dummy_list() // wird später ersetzt
        {
            List<string> remote_dummies  = new List<string>();
            remote_dummies.Add("dummy1");
            remote_dummies.Add("dummy2");
            
            return remote_dummies;
        }

 #region Events
        public async void clicked_fernbedienungsseite()
        {
            await Navigation.PushAsync(new Fernbedienungsseite());

        }

        public async void clicked_einlernseite()
        {
            await Navigation.PushAsync(new .Einlernseite());
        }
#endregion
    }
}
