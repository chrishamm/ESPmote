using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ESPmote
{
	public partial class App : Application
	{
        private static Model.Datenbank datenbank;
        public static Model.Datenbank Datenbank
        {
            get
            {
                if (datenbank == null)
                {
                    datenbank = new Model.Datenbank(DependencyService.Get<IFileHelper>().GetLocalFilePath("espmote.db"));
                }
                return datenbank;
            }
        }

		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new View.Einrichtungsseite());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
