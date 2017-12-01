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
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(EntryProductKey.Text))
            {
                producktId = EntryProductKey.Text.ToString();
            }
            else
            {
                producktId = "47044601";
            }


            await Navigation.PushAsync(new ReviewsListPage(producktId));
        }
        

        
    }
}
