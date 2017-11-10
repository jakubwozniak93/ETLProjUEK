using HtmlAgilityPack;
using MobiParse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobiParse.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public ViewModel.DetailViewModel viewModel;
        public const string ceneoUrl = "https://www.ceneo.pl/";
        public const string ceneoUrlReviewFirstPage = "#tab=reviews";
        public const string ceneoUrlReviews = "/opinie-";
        public string htmlCode;
        string userName;
        string reviewStatus;
        string url;
        List<string> userList;

        public DetailPage(string producktId)
        {
            InitializeComponent();
            userList = new List<string>();

            viewModel = new ViewModel.DetailViewModel();
            //foreach (string user in userList)
            //{
            //    viewModel.Names.Add(user);
            //}

            BindingContext = viewModel;
            GetHTMLCodeAsync(producktId);
        }


        public async Task GetHTMLCodeAsync(string producktId)
        {
            url = ceneoUrl + producktId + ceneoUrlReviewFirstPage;
            string webPage = await new HttpClient().GetStringAsync(new Uri("https://www.ceneo.pl/55891975/opinie-3"));
            GetDataFromSource(webPage);
            
        }

        public void GetDataFromSource(string sourceCode)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sourceCode);
            HtmlNode[] info = doc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "reviewer-name-line").ToArray();
            foreach (HtmlNode item in info)
            {
                userName = item.InnerText.ToString();
                userName = userName.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                userList.Add(userName);
            }
            ListUser.ItemsSource = userList;
        }
    }
}