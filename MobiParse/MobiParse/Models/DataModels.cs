using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiParse.Models
{
    public class DataModels
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string ReviewStatus { get; set; }
        public string ScoreValue { get; set; }
        public string DateTime { get; set; }
        public string ReviewText { get; set; }
        public string ReviewUseful { get; set; }
        public string ReviewUnuseful { get; set; }
        //public List<string> ProductPros { get; set; }
        //public List<string> ProductCons { get; set; }
    }
}
