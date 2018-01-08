using System;
using SQLite.Net.Attributes;

namespace MobiParse.Models.Sqlite
{
    public class ReviewDataModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ReviewID { get; set; }
    }
}

