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
        static LocalDB categoryData;
        static LocalDB productIdData;
        static LocalDB reviewData;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MobiParse.MainPage());
        }
        
        public static LocalDB CategoryData
        {
            get
            {
                if (categoryData == null)
                {
                    categoryData = new LocalDB(DependencyService.Get<ISqliteConnection>().GetDBPath("MobiParse.db3"));
                }
                return categoryData;
            }
        }

        public static LocalDB ProductIdData
        {
            get
            {
                if (productIdData == null)
                {
                    productIdData = new LocalDB(DependencyService.Get<ISqliteConnection>().GetDBPath("MobiParse.db3"));
                }
                return productIdData;
            }
        }
        public static LocalDB ReviewData
        {
            get
            {
                if (reviewData == null)
                {
                    reviewData = new LocalDB(DependencyService.Get<ISqliteConnection>().GetDBPath("MobiParse.db3"));
                }
                return reviewData;
            }
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
