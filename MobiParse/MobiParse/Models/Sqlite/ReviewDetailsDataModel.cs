using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MobiParse.ViewModel
{
    public class ReviewDetailsDataModel : BaseViewModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string ProductKey { get; set; }
        public string UserName { get; set; }
        public string ReviewStatus { get; set; }
        public string ScoreValue { get; set; }
        public string DateTime { get; set; }
        public string ReviewText { get; set; }
        public string ReviewUseful { get; set; }
        public string ReviewUnuseful { get; set; }
        public string ProductPros { get; set; }
        public string ProductCons { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsVisible { get; set; }

    }
}
