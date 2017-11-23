using MobiParse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class ReviewsListViewModel : BaseViewModel
    {
        private string _productInfoLbl;
        private string _productCodeLbl;
        private string _constProductIdlbl;
        private string _messageLbl;
        private bool _isOverlayVisible;
        private List<ReviewDetailsDataModel> _reviewList;

        public ReviewsListViewModel()
        {
            _constProductIdlbl = "Kod produktu: ";
            _messageLbl = "Trwa proces ETL...";
            ReviewList = new List<ReviewDetailsDataModel>();
        }


        public List<ReviewDetailsDataModel> ReviewList
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

    }
}
