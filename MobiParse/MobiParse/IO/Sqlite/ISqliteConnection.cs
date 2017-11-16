
namespace MobiParse.IO.Sqlite
{
    public interface ISqliteConnection
    {
        SQLite.Net.Async.SQLiteAsyncConnection GetConnection();
        void DropDatabase();
    }
}
