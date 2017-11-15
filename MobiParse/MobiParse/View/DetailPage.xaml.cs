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
        string urlProduct;
        string urlReview;
        List<string> userList;
        List<List<string>> reviewData = new List<List<string>>();

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
            //await GetProductInfo(producktId);
            await GetReviewInfo(producktId);
        }
        
        public async Task GetReviewInfo(string producktId)
        {
            
            urlReview = ceneoUrl + producktId + ceneoUrlReviewFirstPage;
            string reviewInfo = await new HttpClient().GetStringAsync(new Uri(urlReview));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(reviewInfo);
            HtmlNode[] allInfo = doc.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
            foreach (HtmlNode allItem in allInfo)
            {
                List<string> singleReviewData = new List<string>();
                HtmlAgilityPack.HtmlDocument review = new HtmlAgilityPack.HtmlDocument();
                review.LoadHtml(allItem.InnerHtml);
                //get username from code
                HtmlNode nameNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "reviewer-name-line").FirstOrDefault();
                string name = nameNode.InnerText.ToString();
                name = name.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                singleReviewData.Add(name);
                //get user review status
                HtmlNode reviewStatusNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-summary").FirstOrDefault();
                string reviewStatus = reviewStatusNode.InnerText.ToString();
                reviewStatus = reviewStatus.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                singleReviewData.Add(reviewStatus);
                //get user score value
                HtmlNode scoreValueNode = review.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-score-count").FirstOrDefault();
                string scoreValue = scoreValueNode.InnerText.ToString();
                scoreValue = scoreValue.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                singleReviewData.Add(scoreValue);
                //get review datetime
                HtmlNode dateTimeNode = review.DocumentNode.Descendants("time").Where(x => x.Attributes.Contains("datetime")).FirstOrDefault();
                string dateTime = dateTimeNode.InnerText.ToString();
                dateTime = dateTime.Replace("\r\n", string.Empty);
                singleReviewData.Add(dateTime);
                //get review text
                HtmlNode reviewTextNode = review.DocumentNode.Descendants("p").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-body").FirstOrDefault();
                string reviewText = reviewTextNode.InnerText.ToString();
                reviewText = reviewText.Replace("\r\n", string.Empty);
                singleReviewData.Add(reviewText);
                //get review useful
                HtmlNode reviewUsefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-yes js_product-review-vote js_vote-yes").FirstOrDefault();
                string reviewUseful = reviewUsefulNode.InnerText.ToString();
                reviewUseful = reviewUseful.Replace("\r\n", string.Empty);
                singleReviewData.Add(reviewUseful);
                //get review unuseful
                HtmlNode reviewUnusefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-no js_product-review-vote js_vote-no").FirstOrDefault();
                string reviewUnuseful = reviewUnusefulNode.InnerText.ToString();
                reviewUnuseful = reviewUnuseful.Replace("\r\n", string.Empty);
                singleReviewData.Add(reviewUnuseful);
                //get product advantages
                HtmlNode[] productAdvantagesNode = review.DocumentNode.Descendants("li").ToArray();
                //string productAdvantages = productAdvantagesNode.InnerText.ToString();
                //productAdvantages = productAdvantages.Replace("\r\n", string.Empty);
                //reviewData.Add(productAdvantages);
                HtmlNode allInfoPros = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "pros-cell").FirstOrDefault();
                HtmlAgilityPack.HtmlDocument reviewPros = new HtmlAgilityPack.HtmlDocument();
                reviewPros.LoadHtml(allInfoPros.InnerHtml);
                HtmlNode[] InfoPros = reviewPros.DocumentNode.Descendants("li").ToArray();


                HtmlNode allInfoCons = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "cons-cell").FirstOrDefault();
                HtmlAgilityPack.HtmlDocument reviewCons = new HtmlAgilityPack.HtmlDocument();
                reviewCons.LoadHtml(allInfoCons.InnerHtml);
                HtmlNode[] InfoCons = reviewCons.DocumentNode.Descendants("li").ToArray();
                

                reviewData.Add(singleReviewData);
            }
        }


        public async Task GetProductInfo(string producktId)
        {
            urlProduct = ceneoUrl + producktId;
            string productInfo = await new HttpClient().GetStringAsync(new Uri(urlProduct));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(urlProduct);

            foreach (HtmlNode node in doc.DocumentNode.Descendants("li"))
            {
                string currentText = node.InnerText; //Fetching the InnerText of the node
                //Console.WriteLine(currentText); // Testing if prints (It doesn't)
            }

            HtmlNode[] info = doc.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
            //HtmlNode[] info2 = doc.DocumentNode.DescendantNodes
            //var nodes = doc.DocumentNode.Descendants();
            //string result = "";
            //foreach (var node in nodes)
            //{

            //    result = result + node.OuterHtml;

            //}
            foreach (HtmlNode item in info)
            {
                userName = item.InnerText.ToString();
                userName = userName.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                userList.Add(userName);
            }
            //ListUser.ItemsSource = userList;
            
        }
    }
}