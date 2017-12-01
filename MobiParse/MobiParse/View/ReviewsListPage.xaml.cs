﻿using HtmlAgilityPack;
using MobiParse.Models;
using MobiParse.ViewModel;
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
    public partial class ReviewsListPage : ContentPage
    {
        public ViewModel.ReviewsListViewModel viewModel;
        public const string ceneoUrl = "https://www.ceneo.pl/";
        public const string ceneoUrlReviewFirstPage = "#tab=reviews";
        public const string ceneoUrlReviews = "/opinie-";
        public string htmlCode;
        string urlReviews;
        string productInfo;
        string CategoryProductInfo;
        List<string> userList;
        ObservableCollection<ReviewDetailsDataModel> singleReviewData;
        int reviewCounts = 0;
        string name, reviewStatus, scoreValue, dateTime, reviewText, reviewUseful, reviewUnuseful, outerHtml;

        private void ListOfReviews_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = BindingContext as ReviewsListViewModel;
            if (item == null)
                return;

            var review = e.Item as ReviewDetailsDataModel;
            item.HideOrShowDetails(review);
            //viewModel.ReviewList = viewModel.ReviewList;
        }

        public ReviewsListPage(string producktId)
        {
            InitializeComponent();
            userList = new List<string>();

            viewModel = new ViewModel.ReviewsListViewModel();
            viewModel.IsOverlayVisible = true;
            BindingContext = viewModel;
            GetHTMLCodeAsync(producktId);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ReviewDetailsDataModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new ReviewDetailsPage(item));

            // Manually deselect item
            ListOfReviews.SelectedItem = null;
        }



        public object Item { get; private set; }

        async void OnClick(object sender, EventArgs e)  
        {  
            await Navigation.PushAsync(new MainPage());
        } 

        public async Task GetHTMLCodeAsync(string producktId)
        {
            viewModel.ProductCodeLbl = producktId;
            urlReviews = ceneoUrl + producktId + ceneoUrlReviews;
            //var response = await new HttpClient().GetAsync(urlReview, urlReviews);
            //await GetProductInfo(producktId);
            //await GetProductInfo(urlReview);
            await GetReviewInfo(urlReviews);
        }
        
        public async Task GetReviewInfo(string url)
        {
            
            singleReviewData = new ObservableCollection<ReviewDetailsDataModel>();

            string reviewInfo = await new HttpClient().GetStringAsync(new Uri(url+"1"));

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(reviewInfo);


            //var nodes = doc.DocumentNode.ToString();
            //foreach (var node in nodes)
            //{

            //    result = result + node.OuterHtml;

            //}

            HtmlNode[] CategoryOfproductInfoNode = doc.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("itemprop") && x.Attributes["itemprop"].Value == "title").ToArray();
            if (CategoryOfproductInfoNode != null)
            {
                CategoryProductInfo = CategoryOfproductInfoNode[3].InnerText.ToString();
            }

            HtmlNode productInfoNode = doc.DocumentNode.Descendants("h2").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "section-title with-context header-curl").FirstOrDefault();
            productInfo = productInfoNode.InnerText.ToString();
            productInfo = productInfo.Replace(" - Opinie", string.Empty);
            viewModel.ProductInfoLbl = productInfo;
            //HtmlNode[] reviewInfoNodes = doc.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
            HtmlNode reviewsCountNode = doc.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("itemprop") && x.Attributes["itemprop"].Value == "reviewCount").FirstOrDefault();
            int reviewCount = (int.Parse(reviewsCountNode.InnerText.ToString())) / 10;
            List<HtmlNode[]> allInfoNodesArray = new List<HtmlNode[]>();
            for (int i = 1; i <= reviewCount+1; i++)
            {
                reviewInfo = await new HttpClient().GetStringAsync(new Uri(url + i));
                doc.LoadHtml(reviewInfo);
                HtmlNode[] reviewsInfoNodes = doc.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
                ParseData(reviewsInfoNodes);
            }
            viewModel.ReviewList = singleReviewData;
            string reviewsNumber = reviewCounts.ToString();
            viewModel.MessageLbl = "pobrano: " + reviewsNumber + " opinii.";
            await Task.Delay(2000);
            viewModel.IsOverlayVisible = false;
            //HtmlNode[] allInfo = new HtmlNode[reviewInfoNodes.Length + reviewsInfoNodes.Length];
            //Array.Copy(reviewInfoNodes, allInfo, reviewInfoNodes.Length);
            //Array.Copy(reviewsInfoNodes, 0, allInfo, reviewInfoNodes.Length, reviewsInfoNodes.Length);

        }


        public void ParseData(HtmlNode[] nodesTable)
        {
            try
            {

                foreach (HtmlNode allItem in nodesTable)
                {
                    reviewCounts++;
                    HtmlAgilityPack.HtmlDocument review = new HtmlAgilityPack.HtmlDocument();
                    review.LoadHtml(allItem.InnerHtml);

                    //get username from code
                    HtmlNode nameNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "reviewer-name-line").FirstOrDefault();
                    if (nameNode != null)
                    {
                        name = nameNode.InnerText.ToString();
                        name = name.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                    }
                    //get user review status
                    HtmlNode reviewStatusNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-summary").FirstOrDefault();
                    if (reviewStatusNode != null)
                    {
                        reviewStatus = reviewStatusNode.InnerText.ToString();
                        reviewStatus = reviewStatus.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                    }
                    //get user score value
                    HtmlNode scoreValueNode = review.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-score-count").FirstOrDefault();
                    if (scoreValueNode != null)
                    {
                        scoreValue = scoreValueNode.InnerText.ToString();
                        scoreValue = scoreValue.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                    }
                    //get review datetime
                    HtmlNode dateTimeNode = review.DocumentNode.Descendants("time").Where(x => x.Attributes.Contains("datetime")).FirstOrDefault();
                    if (dateTimeNode != null)
                    {
                        dateTime = dateTimeNode.InnerText.ToString();
                        dateTime = dateTime.Replace("\r\n", string.Empty);
                    }
                    //get review datetime
                    //HtmlNode dateTimeNode = review.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-time").FirstOrDefault();
                    //if (dateTimeNode != null)
                    //{
                    //    outerHtml = dateTimeNode.OuterHtml.ToString();
                    //    char splitter = '\"';
                    //    string[] outerHtml2 = outerHtml.Split();
                    //    dateTime = outerHtml2[4];
                    //}
                    //get review text
                    HtmlNode reviewTextNode = review.DocumentNode.Descendants("p").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-body").FirstOrDefault();
                    if (reviewTextNode != null)
                    {
                        reviewText = reviewTextNode.InnerText.ToString();
                        reviewText = reviewText.Replace("\r\n", string.Empty);
                    }
                    //get review useful
                    HtmlNode reviewUsefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-yes js_product-review-vote js_vote-yes").FirstOrDefault();
                    if (reviewUsefulNode != null)
                    {
                        reviewUseful = reviewUsefulNode.InnerText.ToString();
                        reviewUseful = reviewUseful.Replace("\r\n", string.Empty);
                    }
                    //get review unuseful
                    HtmlNode reviewUnusefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-no js_product-review-vote js_vote-no").FirstOrDefault();
                    if (reviewUnusefulNode != null)
                    {
                        reviewUnuseful = reviewUnusefulNode.InnerText.ToString();
                        reviewUnuseful = reviewUnuseful.Replace("\r\n", string.Empty);
                    }
                    //get product pros
                    HtmlNode allInfoPros = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "pros-cell").FirstOrDefault();
                    HtmlAgilityPack.HtmlDocument reviewPros = new HtmlAgilityPack.HtmlDocument();
                    reviewPros.LoadHtml(allInfoPros.InnerHtml);
                    HtmlNode[] InfoPros = reviewPros.DocumentNode.Descendants("li").ToArray();
                    string productPros = "";
                    int i = 0;
                    foreach (HtmlNode allPros in InfoPros)
                    {
                        i++;
                        productPros = productPros + i.ToString() + ".) " + allPros.InnerText.ToString() + "\n";

                    }
                    //get product cons
                    HtmlNode allInfoCons = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "cons-cell").FirstOrDefault();
                    HtmlAgilityPack.HtmlDocument reviewCons = new HtmlAgilityPack.HtmlDocument();
                    reviewCons.LoadHtml(allInfoCons.InnerHtml);
                    HtmlNode[] InfoCons = reviewCons.DocumentNode.Descendants("li").ToArray();
                    string productCons = "";
                    i = 0;
                    foreach (HtmlNode allCons in InfoCons)
                    {
                        i++;
                        productCons = productCons + i.ToString() + ".) " + allCons.InnerText.ToString() + "\n";
                    }


                    singleReviewData.Add(new ReviewDetailsDataModel()
                    {
                        UserName = name,
                        ReviewStatus = reviewStatus,
                        ScoreValue = scoreValue,
                        DateTime = dateTime,
                        ReviewText = reviewText,
                        ReviewUseful = reviewUseful,
                        ReviewUnuseful = reviewUnuseful,
                        ProductPros = productPros,
                        ProductCons = productCons,
                        ReviewsCount = reviewCounts,
                        IsVisible = false
                        
                    });
                }
            }catch(Exception ex)
            {

            }
             
        }
        
    }
}