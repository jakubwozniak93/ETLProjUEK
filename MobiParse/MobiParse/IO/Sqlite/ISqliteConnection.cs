
namespace MobiParse.IO.Sqlite
{
    public interface ISqliteConnection
    {
        SQLite.Net.Async.SQLiteAsyncConnection GetConnection();
        string GetDBPath(string fileName);
        void DropDatabase();
    }
}
