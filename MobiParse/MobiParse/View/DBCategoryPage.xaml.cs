using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobiParse.Models;
using MobiParse.ViewModel;
using Xamarin.Forms;

namespace MobiParse.View
{
    public partial class DBCategoryPage : ContentPage
    {
        DBCategoryViewModel viewModel;
        ObservableCollection<CategoryDataModels> singleCategoryData;
        public DBCategoryPage(IList<CategoryDataModels> categoryData)
        {
            InitializeComponent();
            viewModel = new DBCategoryViewModel();
            BindingContext = viewModel;
            singleCategoryData = new ObservableCollection<CategoryDataModels>();
            foreach (CategoryDataModels CategoryModel in categoryData)
            {
                singleCategoryData.Add(new CategoryDataModels()
                {
                    CategoryId = CategoryModel.CategoryId,
                    CategoryName = CategoryModel.CategoryName
                });
            }
            viewModel.CategoryList = singleCategoryData;
        }

        private async void ListOfCategory_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = BindingContext as DBCategoryViewModel;
            if (item == null)
                return;

            var product = e.Item as CategoryDataModels;
            IList<ProductDataModels> categoryDataList = await App.ProductIdData.GetProductByCategoryName(product.CategoryName);

            await Navigation.PushAsync(new DBProductReaderPage(categoryDataList));
        }

        async void OnClick(object sender, EventArgs e)  
        {  
            await Navigation.PushAsync(new MainPage());
        } 
    }
}
