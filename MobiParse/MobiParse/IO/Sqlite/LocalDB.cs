using MobiParse.Models;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobiParse.IO.Sqlite
{
    public class LocalDB
    {
        private SQLiteAsyncConnection db { get; set; }
        public LocalDB()
        {
            Init();
        }

        private async void Init()
        {
            db = DependencyService.Get<ISqliteConnection>().GetConnection();

            CreateTablesResult createdCategoryTable = await db.CreateTableAsync<CategoryDataModels>();
            CreateTablesResult createdProductTable = await db.CreateTableAsync<ProductDataModels>();

        }

        internal void ClearAll()
        {
            DependencyService.Get<ISqliteConnection>().DropDatabase();
            Init();
        }

        internal async Task<IList<CategoryDataModels>> GetExamples()
        {
            List<CategoryDataModels> list = await db.Table<CategoryDataModels>().ToListAsync();
            return list;
        }

        internal async Task<CategoryDataModels> GetWork(int id)
        {
            CategoryDataModels w = await db.Table<CategoryDataModels>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return w;
        }

        internal async Task<bool> InserExample(CategoryDataModels example)
        {
            int res = await db.InsertAsync(example);
            return res != -1;
        }

        internal async Task<bool> UpdateWork(CategoryDataModels example)
        {
            int res = await db.UpdateAsync(example);
            return res != -1;
        }
    }
}
