using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobiParse.Models;
using MobiParse.ViewModel;
using Xamarin.Forms;

namespace MobiParse.View
{
    public partial class DBProductReaderPage : ContentPage
    {
        public DBProductViewModel viewModel;
        public ObservableCollection<ProductDataModels> singleproductData;

        public DBProductReaderPage(IList<ProductDataModels> productData)
        {
            InitializeComponent();
            viewModel = new DBProductViewModel();
            BindingContext = viewModel;
            singleproductData = new ObservableCollection<ProductDataModels>();
            foreach(ProductDataModels ProductModel in productData)
            {
                singleproductData.Add(new ProductDataModels()
                {
                    ProductKey = ProductModel.ProductKey,
                    ProductName = ProductModel.ProductName
                });
            }
            viewModel.ProductList = singleproductData;
        }

        private async void ListOfProduct_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = BindingContext as DBProductViewModel;
            if (item == null)
                return;

            var review = e.Item as ProductDataModels;
            IList<ReviewDetailsDataModel> reviewDataList = await App.ReviewData.GetReviewDetailByProductKey(review.ProductKey);

            await Navigation.PushAsync(new DBReviewPage(reviewDataList));
        }

        async void OnClick(object sender, EventArgs e)  
        {  
            await Navigation.PushAsync(new MainPage());
        } 
    }
}
