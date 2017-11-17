using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MobiParse.iOS.IO.Sqlite;
using MobiParse.IO.Sqlite;
using SQLite.Net.Async;
using SQLite.Net;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteConnection))]
namespace MobiParse.iOS.IO.Sqlite
{
    class SqliteConnection : MobiParse.IO.Sqlite.ISqliteConnection
    {
        private const string SqliteFilename = "app.db3";
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
                            //string path = GetDBPath();
                            //var platform = new SQLitePlatformIOS();
                            //conn = new SQLiteConnectionWithLock(platform, new SQLiteConnectionString(path, true));
                        }
                    }
                }
                return conn;
            }
        }
        

        private string GetDBPath()
        {
            FileSystem.FileManager fm = new FileSystem.FileManager();
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

            FileSystem.FileManager fm = new FileSystem.FileManager();
            string path = GetDBPath();
            fm.DeleteFile(path);
        }

        SQLiteAsyncConnection ISqliteConnection.GetConnection()
        {
            var ConnectFuncion = new Func<SQLiteConnectionWithLock>(Connect);
            var conn = new SQLite.Net.Async.SQLiteAsyncConnection(ConnectFuncion);
            return conn;
        }
    }
}