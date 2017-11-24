using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class ReviewDetailsViewModel : BaseViewModel
    {
        ReviewDetailsDataModel Model;
        public string _userName;
        public string _reviewStatus;
        public string _scoreValue;
        public string _dateTime;
        public string _reviewText;
        public string _reviewUseful;
        public string _reviewUnuseful;
        public string _productPros;
        public string _productCons;

        public ReviewDetailsViewModel()
        {
            Model = new ReviewDetailsDataModel();
        }

        public void GetReviewDetails (ReviewDetailsDataModel ReviewDetais)
        {
            UserName = ReviewDetais.UserName;
            ReviewStatus = ReviewDetais.ReviewStatus;
            ScoreValue = ReviewDetais.ScoreValue;
            DateTime = ReviewDetais.DateTime;
            ReviewText = ReviewDetais.ReviewText;
            ReviewUseful = ReviewDetais.ReviewUseful;
            ReviewUnuseful = ReviewDetais.ReviewUnuseful;
            ProductProsToString(ReviewDetais.ProductPros);
            ProductConsToString(ReviewDetais.ProductCons);
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }
        public string ReviewStatus
        {
            get
            {
                return _reviewStatus;
            }
            set
            {
                _reviewStatus = value;
                RaisePropertyChanged(nameof(ReviewStatus));
            }
        }
        public string ScoreValue
        {
            get
            {
                return _scoreValue;
            }
            set
            {
                _scoreValue = value;
                RaisePropertyChanged(nameof(ScoreValue));
            }
        }
        public string DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
                RaisePropertyChanged(nameof(DateTime));
            }
        }
        public string ReviewText
        {
            get
            {
                return _reviewText;
            }
            set
            {
                _reviewText = value;
                RaisePropertyChanged(nameof(ReviewText));
            }
        }
        public string ReviewUseful
        {
            get
            {
                return _reviewUseful;
            }
            set
            {
                _reviewUseful = value;
                RaisePropertyChanged(nameof(ReviewUseful));
            }
        }
        public string ReviewUnuseful
        {
            get
            {
                return _reviewUnuseful;
            }
            set
            {
                _reviewUnuseful = value;
                RaisePropertyChanged(nameof(ReviewUnuseful));
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

        public void ProductProsToString (List<string> productPros)
        {
            string combindedString = string.Join(";", productPros.ToArray());
            combindedString = combindedString.Replace(";", "; \n");
            ProductPros = combindedString;
        }

        public void ProductConsToString(List<string> productCons)
        {
            string combindedString = string.Join(";", productCons.ToArray());
            combindedString.Replace(";","; \n");
            ProductCons = combindedString;
        }

    }
}
