using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using HtmlAgilityPack;
using AngleSharp.Parser.Html;
using MobiParse.View;
using MobiParse.Models;

namespace MobiParse
{
    public partial class MainPage : ContentPage
    {
        string producktId;


        public MainPage()
        {
            InitializeComponent();
            EntryProductKey.TextChanged -= LoginEntry_TextChanged;
            EntryProductKey.TextChanged += LoginEntry_TextChanged;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(EntryProductKey.Text))
            {
                producktId = EntryProductKey.Text.ToString();
            }
            else
            {
                //iphone 8
                producktId = "55381561";
                //iphone 7
                //producktId = "47044601";
                // xiaomi a1
                producktId = "46943265";
            }


            await Navigation.PushAsync(new ExtractPage(producktId));
        }
        
        private async void ExtractClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(EntryProductKey.Text))
            {
                producktId = EntryProductKey.Text.ToString();
            }
            else
            {
                //iphone 8
                producktId = "55381561";
                //iphone 7
                //producktId = "47044601";
            }
            await Navigation.PushAsync(new ExtractListPage(producktId));
        }

        private void LoginEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            bool isValid = EntryProductKey.Text.Length >= 8 && EntryProductKey.Text.Length <= 8;

            if (isValid)
            {
                etlButton.IsEnabled = true;
                etlButton.BackgroundColor = Color.FromHex("#F39729");
                extractButton.Source = "e_active.png";
                extractButton.InputTransparent = false;
            }
            else
            {
                etlButton.IsEnabled = false;
                etlButton.BackgroundColor = Color.FromHex("#303030");
                extractButton.Source = "e_inactive.png";
                extractButton.InputTransparent = true;
            }
        }

    }
}
