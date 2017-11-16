using MobiParse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private string _productInfoLbl;
        private string _productCodeLbl;
        private string _constProductIdlbl;
        private List<ReviewDetailsViewModel> _reviewList;

        public DetailViewModel()
        {
            _constProductIdlbl = "Kod produktu: ";
            ReviewList = new List<ReviewDetailsViewModel>();
        }


        public List<ReviewDetailsViewModel> ReviewList
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

    }
}
