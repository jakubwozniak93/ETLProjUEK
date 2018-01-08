using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobiParse.ViewModel;

namespace MobiParse.Models
{
    [Table("Category")]
    public class CategoryDataModels : BaseViewModel
    {
        [AutoIncrement, PrimaryKey]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
