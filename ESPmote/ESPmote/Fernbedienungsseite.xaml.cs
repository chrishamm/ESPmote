using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ESPmote
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public  partial class Fernbedienungsseite : ContentPage
	{
		public Fernbedienungsseite ()
		{
			InitializeComponent ();
            button_grid_erstellen(dummy_befehlsliste());
		}

	    public void button_grid_erstellen(Dictionary<string,string> dict)
	    {   
            Command command_infrarot = new Command(infrarot_sender);

	        int i = dict.Count;
	        double reihen = i / 3;

	        if (i % 3 !=0)
	        {
	            reihen++;
	        }

	        StackLayout layout = new StackLayout();
            Grid grid = new Grid();
            
	        layout.Children.Add(grid);

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
	        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
	        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int k = 0; i <= reihen; k++)
	        {
	            grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(1,GridUnitType.Star)});
	        }



	        int reihe = 0;
	        int spalte = 0;
            
	                     foreach (string befehle in dict.Keys)
	                        {
                                grid.Children.Add(new Button
                                {
                                    FontSize =(Device.GetNamedSize(NamedSize.Medium,typeof(Button))/3),
                                    Text = befehle,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    Command = command_infrarot,
                                },spalte,reihe);
	                            spalte++;
	                            if (spalte == 3)
	                            {
	                                reihe++;
	                                spalte = 0;
	                            }
	                        }
           
	        Content = layout;





	    }

	    public Dictionary<string,string> dummy_befehlsliste()
	    {
	        Dictionary<string, string> befehlsliste = new Dictionary<string, string>();
            befehlsliste.Add("Volume_up","infraredcode");
	        befehlsliste.Add("Volume_down", "infraredcode2");
	        befehlsliste.Add("off", "infraredcode3");
	        befehlsliste.Add("on", "infraredcode4");

	        return befehlsliste;

	    }

	    public void infrarot_sender()
	    {
	        //infrarot schicken
	    }
	}
}