using System;
using System.IO;
using Xamarin.Forms;

namespace SHL.TimeSheet
{
    public partial class App : Application
    {
        public static string FolderPath { get; private set; }
        public static ARGUMENT Argument { get; set; }

        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            SQLitePCL.Batteries_V2.Init();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
