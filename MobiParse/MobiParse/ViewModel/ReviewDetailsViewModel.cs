using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.ViewModel
{
    public class ReviewDetailsViewModel : BaseViewModel
    {
        ReviewDetailsDataModel Model { get; set; }
        public ReviewDetailsViewModel(ReviewDetailsDataModel model)
        {
            Model = model;
        }
    }
}
