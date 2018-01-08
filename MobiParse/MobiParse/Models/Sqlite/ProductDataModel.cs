using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.Models
{
    public class ProductDataModels
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductKey { get; set; }
    }
}
