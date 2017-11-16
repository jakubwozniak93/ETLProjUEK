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

            CreateTablesResult created = await db.CreateTableAsync<DataModels>();
        }

        internal void ClearAll()
        {
            DependencyService.Get<ISqliteConnection>().DropDatabase();
            Init();
        }

        internal async Task<IList<DataModels>> GetExamples()
        {
            List<DataModels> list = await db.Table<DataModels>().ToListAsync();
            return list;
        }

        internal async Task<DataModels> GetWork(int id)
        {
            DataModels w = await db.Table<DataModels>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return w;
        }

        internal async Task<bool> InserExample(DataModels example)
        {
            int res = await db.InsertAsync(example);
            return res != -1;
        }

        internal async Task<bool> UpdateWork(DataModels example)
        {
            int res = await db.UpdateAsync(example);
            return res != -1;
        }
    }
}
