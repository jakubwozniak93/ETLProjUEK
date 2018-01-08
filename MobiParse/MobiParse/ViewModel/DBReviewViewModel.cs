using System;
using System.Collections.ObjectModel;

namespace MobiParse.ViewModel
{
    public class DBReviewViewModel : BaseViewModel
    {
        ObservableCollection<ReviewDetailsDataModel> _reviewList;

        public DBReviewViewModel()
        {
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
    }
}
