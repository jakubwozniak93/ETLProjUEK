using MobiParse.IO.FileSystem;
using MobiParse.IO.Sqlite;
using MobiParse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobiParse
{
    public partial class App : Application
    {
        static CategoryDataModels categoryData;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MobiParse.MainPage());
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

        public static void Configure()
        {
            throw new NotImplementedException();
        }
    }
}
