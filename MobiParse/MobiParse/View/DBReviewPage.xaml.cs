using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobiParse.ViewModel;
using Xamarin.Forms;

namespace MobiParse.View
{
    public partial class DBReviewPage : ContentPage
    {
        public ReviewsListViewModel viewModel;
        public ObservableCollection<ReviewDetailsDataModel> singleReviewData;
        ReviewDetailsDataModel currentProductId;

        public DBReviewPage(IList<ReviewDetailsDataModel> reviewData)
        {
            InitializeComponent();
            viewModel = new ReviewsListViewModel();
            BindingContext = viewModel;
            singleReviewData = new ObservableCollection<ReviewDetailsDataModel>();
            foreach (ReviewDetailsDataModel ReviewModel in reviewData)
            {
                singleReviewData.Add(new ReviewDetailsDataModel()
                {
                    ReviewID = ReviewModel.ReviewID,
                    CategoryName = ReviewModel.CategoryName,
                    ProductName = ReviewModel.ProductName,
                    ProductKey = ReviewModel.ProductKey,
                    UserName = ReviewModel.UserName,
                    ReviewStatus = ReviewModel.ReviewStatus,
                    ScoreValue = ReviewModel.ScoreValue,
                    DateTime = ReviewModel.DateTime,
                    ReviewText = ReviewModel.ReviewText,
                    ReviewUseful = ReviewModel.ReviewUseful,
                    ReviewUnuseful = ReviewModel.ReviewUnuseful,
                    ProductPros = ReviewModel.ProductPros,
                    ProductCons = ReviewModel.ProductCons,
                    ReviewsCount = ReviewModel.ReviewsCount,
                    IsVisible = ReviewModel.IsVisible
                });
            }
            viewModel.ReviewList = singleReviewData;
        }

        private async void ListOfReview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = BindingContext as DBProductViewModel;
            if (item == null)
                return;

            var review = e.Item as ReviewDetailsDataModel;
            if (currentProductId == null && currentProductId != review)
            {
                currentProductId = review;
            }
            else
            {
                currentProductId = null;
            }
            //item.HideOrShowDetails(review);

        }

        async void OnClick(object sender, EventArgs e)  
        {  
            await Navigation.PushAsync(new MainPage());
        } 

        async Task OnDeleteIconTappedAsync(object sender, EventArgs args)
        {
            await App.ReviewData.DeleteReviewAsync(currentProductId.ReviewID);

            viewModel.ReviewList.Remove(currentProductId);

        }

        void OnSaveIconTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
        }
    }
}
