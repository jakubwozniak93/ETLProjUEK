using System;

using Xamarin.Forms;

namespace MobiParse.View
{
    public class DBProductReaderPage : ContentPage
    {
        public DBProductReaderPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

