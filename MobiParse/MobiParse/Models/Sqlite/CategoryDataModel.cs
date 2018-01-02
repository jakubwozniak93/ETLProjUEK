using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.Models
{
    public class CategoryDataModels
    {
        [AutoIncrement, PrimaryKey]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
