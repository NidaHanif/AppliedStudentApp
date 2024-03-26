﻿using System.Data.SQLite;
using System.Data;
using System.Reflection.Metadata;
using AppliedStudent.Pages.School;
using System.Text;
using AppliedStudent.Codes;

namespace AppliedTax.Codes
{
    public class StudentTable
    {
        public SQLiteConnection MyConnection { get; set; }
        public DataTable MyDataTable { get; set; }
        public DataView MyDataView { get; set; }
        public string TableName { get; set; }

        public StudentTable()
        {
            MyConnection = ConnectionClass.GetConnected();
            MyDataTable = new DataTable();
            TableName = string.Empty;
            MyDataView = new DataView();
        }

        // Constructor
        public StudentTable(string TableName)
        {
            MyDataTable = new DataTable();

            MyConnection = ConnectionClass.GetConnected();
            string Text = $"SELECT * FROM {TableName}";
            SQLiteCommand _Command = new(Text, MyConnection);
            SQLiteDataAdapter _Adapter = new(_Command);
            DataSet _DataSet = new DataSet();
            _Adapter.Fill(_DataSet, TableName);

            if (_DataSet.Tables.Count > 0)
            {
                MyDataTable = _DataSet.Tables[0];
                MyDataView = MyDataTable.AsDataView();
                TableName = MyDataTable.TableName;
            }
        }

        public static DataRow GetRow(string TableName, int ID)
        {
            StudentTable _StudentTable = new(TableName);
            _StudentTable.MyDataView.RowFilter = $"[ID]={ID}";
            if (_StudentTable.MyDataView.Count == 1)
            {
                return _StudentTable.MyDataView[0].Row;
            }

            return _StudentTable.MyDataTable.NewRow();
        }

        public static DataRow GetRow(string TableName, string RecordCode)
        {
            StudentTable _StudentTable = new(TableName);
            _StudentTable.MyDataView.RowFilter = $"[ID]='{RecordCode}'";
            if (_StudentTable.MyDataView.Count > 0)
            {
                return _StudentTable.MyDataView[0].Row;
            }

            return _StudentTable.MyDataTable.NewRow();
        }

        public static string GetTitle(string TableName, long RecID)
        {
            string _Result = string.Empty;
            string Text = $"SELECT * FROM {TableName}";
            SQLiteCommand _Command = new(Text, ConnectionClass.GetConnected());
            SQLiteDataAdapter _Adapter = new(_Command);
            DataSet _DataSet = new DataSet();
            _Adapter.Fill(_DataSet);

            if (_DataSet.Tables.Count > 0)
            {
                var MyDataTable = _DataSet.Tables[0];
                var MyDataView = MyDataTable.AsDataView();

                MyDataView.RowFilter = $"ID={RecID}";
                if (MyDataView.Count == 1)
                {
                    _Result = (string)MyDataView[0]["Title"];
                }
            }

            return _Result;
        }

        public static DataTable GetTable(string TableName)
        {

            string Text = $"SELECT * FROM {TableName}";
            SQLiteCommand _Command = new(Text, ConnectionClass.GetConnected());
            SQLiteDataAdapter _Adapter = new(_Command);
            DataSet _DataSet = new DataSet();
            _Adapter.Fill(_DataSet, TableName);
            if (_DataSet.Tables.Count > 0)
            {
                return _DataSet.Tables[0];
            }
            return new DataTable();

        }

    }

}