using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobiParse.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobiParse.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewDetailsPage : ContentPage
    {
        public ReviewDetailsViewModel viewModel;

        public ReviewDetailsPage(ReviewDetailsDataModel item)
        {
            InitializeComponent();
            viewModel = new ReviewDetailsViewModel();
            BindingContext = viewModel;
            viewModel.GetReviewDetails(item);
        }

        async void OnClick(object sender, EventArgs e)  
        {  
            await Navigation.PushAsync(new MainPage());
        } 
    }
}