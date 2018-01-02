using System;
using MobiParse.Droid.IO.Sqlite;
using MobiParse.IO.Sqlite;
using SQLite.Net.Async;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteConnection))]
namespace MobiParse.Droid.IO.Sqlite
{
    class SqliteConnection : MobiParse.IO.Sqlite.ISqliteConnection
    {
        private const string SqliteFilename = "MobiParse.db3";

        object objLock = new object();
        private SQLiteConnectionWithLock conn = null;

        public SQLiteConnectionWithLock Conn
        {
            get
            {
                if (conn == null)
                {
                    lock (objLock)
                    {
                        if (conn == null)
                        {
                            string path = GetDBPath();
                            var platform = new SQLitePlatformAndroidN();
                            conn = new SQLiteConnectionWithLock(platform, new SQLiteConnectionString(path, true));
                        }
                    }
                }
                return conn;
            }
        }
        

        private string GetDBPath()
        {
            IO.FileManager.FileManager fm = new IO.FileManager.FileManager();
            fm.TryRecreateWorkspace();
            string path = System.IO.Path.Combine(fm.DatabaseDirectory, SqliteFilename);
            return path;
        }

        private SQLiteConnectionWithLock Connect()
        {
            return Conn;
        }

        public void DropDatabase()
        {
            if (conn != null)
            {
                conn.Rollback();
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            IO.FileManager.FileManager fm = new IO.FileManager.FileManager();
            string path = GetDBPath();
            fm.DeleteFile(path);
        }

        SQLiteAsyncConnection ISqliteConnection.GetConnection()
        {
            var ConnectFuncion = new Func<SQLiteConnectionWithLock>(Connect);
            var conn = new SQLite.Net.Async.SQLiteAsyncConnection(ConnectFuncion);
            return conn;
        }

        string ISqliteConnection.GetDBPath(string dbPath)
        {
            return GetDBPath();
        }
    }
}