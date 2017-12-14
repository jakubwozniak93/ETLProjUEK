using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class ExtractListViewModel :BaseViewModel
    {
        private string _productInfoLbl;
        private string _productCodeLbl;
        private string _constProductIdlbl;
        private string _messageLbl;
        private bool _isOverlayVisible;
        private ReviewDetailsDataModel _oldReview;
        private ObservableCollection<ReviewDetailsDataModel> _reviewList;
        public string _productPros;
        public string _productCons;

        public ExtractListViewModel()
        {
            _constProductIdlbl = "Kod produktu: ";
            _messageLbl = "Trwa proces EXTRACT...";
            ReviewList = new ObservableCollection<ReviewDetailsDataModel>();

        }


        public ObservableCollection<ReviewDetailsDataModel> ReviewList
        {
            get
            {
                return _reviewList;
            }
            set
            {
                _reviewList = value;
                RaisePropertyChanged(nameof(ReviewList));
            }
        }

        public string ProductInfoLbl
        {
            get
            {
                return _productInfoLbl;
            }
            set
            {
                _productInfoLbl = value;
                RaisePropertyChanged(nameof(ProductInfoLbl));
            }
        }

        public void HideOrShowDetails(ReviewDetailsDataModel review)
        {
            if (_oldReview == review)
            {
                review.IsVisible = !review.IsVisible;
                UpdateReviews(review);
            }
            else
            {
                if (_oldReview != null)
                {
                    _oldReview.IsVisible = false;
                    UpdateReviews(_oldReview);
                }
                review.IsVisible = true;
                UpdateReviews(review);
            }
            _oldReview = review;
        }

        private void UpdateReviews(ReviewDetailsDataModel review)
        {
            var index = ReviewList.IndexOf(review);
            ReviewList.Remove(review);
            ReviewList.Insert(index, review);
            //ReviewList = ReviewList;
        }

        public string ProductCodeLbl
        {
            get
            {
                return _constProductIdlbl + _productCodeLbl;
            }
            set
            {
                _productCodeLbl = value;
                RaisePropertyChanged(nameof(ProductCodeLbl));
            }
        }

        public bool IsOverlayVisible
        {
            get
            {
                return _isOverlayVisible;
            }
            set
            {
                _isOverlayVisible = value;
                RaisePropertyChanged(nameof(IsOverlayVisible));
            }
        }

        public string MessageLbl
        {
            get
            {
                return _messageLbl;
            }
            set
            {
                _messageLbl = value;
                RaisePropertyChanged(nameof(MessageLbl));
            }
        }

        public string ProductPros
        {
            get
            {
                return _productPros;
            }
            set
            {
                _productPros = value;
                RaisePropertyChanged(nameof(ProductPros));
            }
        }
        public string ProductCons
        {
            get
            {
                return _productCons;
            }
            set
            {
                _productCons = value;
                RaisePropertyChanged(nameof(ProductCons));
            }
        }

        public void ProductProsToString(List<string> productPros)
        {
            string combindedString = string.Join(";", productPros.ToArray());
            combindedString = combindedString.Replace(";", "; \n");
            ProductPros = combindedString;
        }

        public void ProductConsToString(List<string> productCons)
        {
            string combindedString = string.Join(";", productCons.ToArray());
            combindedString.Replace(";", "; \n");
            ProductCons = combindedString;
        }
    }
}
