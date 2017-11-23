using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobiParse.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewDetailsPage : ContentPage
    {
        public ViewModel.ReviewsListViewModel viewModel;

        public ReviewDetailsPage()
        {
            InitializeComponent();
            viewModel = new ViewModel.ReviewsListViewModel();
            BindingContext = viewModel;
        }
    }
}