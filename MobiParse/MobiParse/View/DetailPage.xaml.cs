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
    public partial class DetailPage : ContentPage
    {
        public ViewModel.DetailViewModel viewModel;
        public const string ceneoUrl = "https://www.ceneo.pl/";
        public const string ceneoUrlReviewFirstPage = "#tab=reviews";
        public const string ceneoUrlReviews = "/opinie-21";
        public string htmlCode;
        string userName;
        string reviewStatus;
        string urlProduct;
        string urlReview;
        string urlReviews;
        string productInfo;
        List<string> userList;
        List<ReviewDetailsViewModel> singleReviewData;

        public DetailPage(string producktId)
        {
            InitializeComponent();
            userList = new List<string>();

            viewModel = new ViewModel.DetailViewModel();

            BindingContext = viewModel;
            GetHTMLCodeAsync(producktId);
        }


        public async Task GetHTMLCodeAsync(string producktId)
        {
            viewModel.ProductCodeLbl = producktId;
            urlReview = ceneoUrl + producktId + ceneoUrlReviewFirstPage;
            urlReviews = ceneoUrl + producktId + ceneoUrlReviews;
            //var response = await new HttpClient().GetAsync(urlReview, urlReviews);
            //await GetProductInfo(producktId);
            //await GetProductInfo(urlReview);
            await GetReviewInfo(urlReview, urlReviews);
        }
        
        public async Task GetReviewInfo(string url, string url2)
        {
            
            singleReviewData = new List<ReviewDetailsViewModel>();
            string reviewInfo = await new HttpClient().GetStringAsync(new Uri(url));
            string reviewsInfo = await new HttpClient().GetStringAsync(new Uri(url2));


            

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(reviewInfo);
            HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            doc2.LoadHtml(reviewsInfo);

            var nodes = doc.DocumentNode.ToString();
            string result = "";
            //foreach (var node in nodes)
            //{

            //    result = result + node.OuterHtml;

            //}

            HtmlNode productInfoNode = doc.DocumentNode.Descendants("h2").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "section-title with-context header-curl").FirstOrDefault();
            productInfo = productInfoNode.InnerText.ToString();
            productInfo = productInfo.Replace(" - Opinie", string.Empty);
            viewModel.ProductInfoLbl = productInfo;
            HtmlNode[] reviewInfoNodes = doc.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
            HtmlNode[] reviewsInfoNodes = doc2.DocumentNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-box js_product-review").ToArray();
            
            HtmlNode[] allInfo = new HtmlNode[reviewInfoNodes.Length + reviewsInfoNodes.Length];
            Array.Copy(reviewInfoNodes, allInfo, reviewInfoNodes.Length);
            Array.Copy(reviewsInfoNodes, 0, allInfo, reviewInfoNodes.Length, reviewsInfoNodes.Length);
            foreach (HtmlNode allItem in allInfo)
            {
                
                HtmlAgilityPack.HtmlDocument review = new HtmlAgilityPack.HtmlDocument();
                review.LoadHtml(allItem.InnerHtml);

                //get username from code
                HtmlNode nameNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "reviewer-name-line").FirstOrDefault();
                string name = nameNode.InnerText.ToString();
                name = name.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                //get user review status
                HtmlNode reviewStatusNode = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-summary").FirstOrDefault();
                string reviewStatus = reviewStatusNode.InnerText.ToString();
                reviewStatus = reviewStatus.Replace("\r\n", string.Empty);
                //get user score value
                HtmlNode scoreValueNode = review.DocumentNode.Descendants("span").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "review-score-count").FirstOrDefault();
                string scoreValue = scoreValueNode.InnerText.ToString();
                scoreValue = scoreValue.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
                //get review datetime
                HtmlNode dateTimeNode = review.DocumentNode.Descendants("time").Where(x => x.Attributes.Contains("datetime")).FirstOrDefault();
                string dateTime = dateTimeNode.InnerText.ToString();
                dateTime = dateTime.Replace("\r\n", string.Empty);
                //get review text
                HtmlNode reviewTextNode = review.DocumentNode.Descendants("p").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "product-review-body").FirstOrDefault();
                string reviewText = reviewTextNode.InnerText.ToString();
                reviewText = reviewText.Replace("\r\n", string.Empty);
                //get review useful
                HtmlNode reviewUsefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-yes js_product-review-vote js_vote-yes").FirstOrDefault();
                string reviewUseful = reviewUsefulNode.InnerText.ToString();
                reviewUseful = reviewUseful.Replace("\r\n", string.Empty);
                //get review unuseful
                HtmlNode reviewUnusefulNode = review.DocumentNode.Descendants("button").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "vote-no js_product-review-vote js_vote-no").FirstOrDefault();
                string reviewUnuseful = reviewUnusefulNode.InnerText.ToString();
                reviewUnuseful = reviewUnuseful.Replace("\r\n", string.Empty);
                //get product pros
                HtmlNode allInfoPros = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "pros-cell").FirstOrDefault();
                HtmlAgilityPack.HtmlDocument reviewPros = new HtmlAgilityPack.HtmlDocument();
                reviewPros.LoadHtml(allInfoPros.InnerHtml);
                HtmlNode[] InfoPros = reviewPros.DocumentNode.Descendants("li").ToArray();
                List<string> productPros = new List<string>();
                foreach (HtmlNode allPros in InfoPros)
                {
                    productPros.Add(allPros.InnerText.ToString());
                }
                //get product cons
                HtmlNode allInfoCons = review.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "cons-cell").FirstOrDefault();
                HtmlAgilityPack.HtmlDocument reviewCons = new HtmlAgilityPack.HtmlDocument();
                reviewCons.LoadHtml(allInfoCons.InnerHtml);
                HtmlNode[] InfoCons = reviewCons.DocumentNode.Descendants("li").ToArray();
                List<string> productCons = new List<string>();
                foreach (HtmlNode allCons in InfoCons)
                {
                    productCons.Add(allCons.InnerText.ToString());
                }

                singleReviewData.Add(new ReviewDetailsViewModel()
                {
                    UserName = name,
                    ReviewStatus = reviewStatus,
                    ScoreValue = scoreValue,
                    DateTime = dateTime,
                    ReviewText = reviewText,
                    ReviewUseful = reviewUseful,
                    ReviewUnuseful = reviewUnuseful,
                    ProductPros = productPros,
                    ProductCons = productCons

                });
                
            }
            viewModel.ReviewList = singleReviewData; 
        }


        public async Task GetProductInfo(string url)
        {
            string productInfo = await new HttpClient().GetStringAsync(new Uri(url));
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(productInfo);
            
            //var nodes = doc.DocumentNode.h;
            //string result = "";
            //foreach (var node in nodes)
            //{

            //    result = result + node.OuterHtml;

            //}
            
        }
    }
}