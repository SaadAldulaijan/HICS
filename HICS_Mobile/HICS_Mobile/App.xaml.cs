using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HICS_Mobile.Services;
using HICS_Mobile.Views;

namespace HICS_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
