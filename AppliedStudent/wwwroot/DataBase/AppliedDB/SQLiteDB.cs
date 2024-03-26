using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace AppliedDB
{
    public class SQLiteDB
    {

        public static string GetTitle(string DBFile, Enums.Tables _Table, int Id)
        {
            if (Id > 0)
            {
                var _DataList = DataSource.GetDataList(DBFile, _Table);
                var _Title = _DataList.FirstOrDefault(e => e.Keys.Contains(Id)).First().Value;
                if(_Title != null ) { return _Title; }
            }
            return "";
        }

        public class AppliedConnection
        {
            //public string UserID { get; set; }
            //public string DBPath { get; set; }
            //public string DBFile { get; set; }

            public static string GetRootPath()
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "SQLiteDB");
            }

            public static SQLiteConnection GetUsersConnection()
            {
                var DBPath = GetRootPath();
                var DBFile = "AppliedUsers.db";


                SQLiteConnection _Connection = new();
                try
                {
                    _Connection.ConnectionString = $"Data Source={DBPath}\\{DBFile}";
                    _Connection.Open();
                }
                catch (Exception)
                {
                    //string msg = AppErrors.GetErrorMessage(EnumErrors.Conn_NotEstablished);
                }
                return _Connection;
            }


            public static SQLiteConnection GetConnection(string _DBfile)
            {

                var DBPath = GetRootPath();
                var DBFile = _DBfile;
                SQLiteConnection _Connection = new();

                try
                {
                    var _File = $"{DBPath}\\{DBFile}";
                    if (File.Exists(_File))
                    {
                        _Connection.ConnectionString = $"Data Source={_File}";
                        _Connection.Open();
                    }
                }
                catch (Exception)
                {
                    //string msg = AppErrors.GetErrorMessage(AppErrors.GetErrorMessage();

                }
                return _Connection;
            }



        }

    }
}