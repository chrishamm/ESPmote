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

            StackLayout layout = new StackLayout();


            foreach (string elem in remotelist)
            {
               layout.Children.Add(new Button{Text = elem,HorizontalOptions = LayoutOptions.Start});

            }
            Content = layout;

        }

        public List<string> dummy_list() // wird später ersetzt
        {
            List<string> remote_dummies  = new List<string>();
            remote_dummies.Add("dummy1");
            remote_dummies.Add("neue fernbedienung");
            return remote_dummies;
        }
}
}
