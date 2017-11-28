using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.Models
{
    public class ReviewDataModels
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string ReviewId { get; set; }
    }
}
