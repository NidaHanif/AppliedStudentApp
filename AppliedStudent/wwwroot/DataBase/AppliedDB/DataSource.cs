using System.Data.SQLite;
using System.Data;


namespace AppliedDB
{
    public static class DataSource
    {
        public static DataTable GetDataTable(string _DBfile, Enums.Tables _Table)
        {
                string TableName = _Table.ToString();
                return GetDataTable(_DBfile, TableName);
        }
        public static DataTable GetDataTable(string _DBfile, string _Table)
        {
            try
            {
                string TableName = _Table.ToString();
                var _CommandText = $"SELECT * FROM [{TableName}]";
                SQLiteCommand _Command = new(_CommandText, SQLiteDB.AppliedConnection.GetConnection(_DBfile));
                SQLiteDataAdapter _Adapter = new(_Command);
                DataSet _DataSet = new();
                _Adapter.Fill(_DataSet, TableName);

                if (_DataSet.Tables.Count == 1)
                {
                    _Command.Dispose();
                    _Adapter.Dispose();
                    return _DataSet.Tables[0];
                }
                else
                {
                    return new DataTable();
                }

            }
            catch (SQLiteException)
            {
                return new DataTable();
            }

            catch (Exception)
            {

                return new DataTable();
            }

        }
        public static DataTable GetDataTable(string DBFile, string _Query, string _TableName)
        {
            DataTable _DataTable;
            var _CommandText = _Query;
            SQLiteCommand _Command = new(_CommandText, SQLiteDB.AppliedConnection.GetConnection(DBFile));
            SQLiteDataAdapter _Adapter = new(_Command);
            DataSet _DataSet = new();
            _Adapter.Fill(_DataSet, _TableName);

            if (_DataSet.Tables.Count == 1)
            {
                _DataTable = _DataSet.Tables[0];
            }
            else
            {
                _DataTable = new DataTable();
            }
            
            _Command.Dispose();
            _Adapter.Dispose();
            _DataSet.Dispose();

            return _DataTable;
        }
        public static List<Dictionary<int, string>> GetDataList(string DBFile, Enums.Tables _Table)
        {
            List<Dictionary<int, string>> _List = new();
            try
            {
                string TableName = _Table.ToString();
                var _CommandText = $"SELECT * FROM [{TableName}]";
                SQLiteCommand _Command = new(_CommandText, SQLiteDB.AppliedConnection.GetConnection(DBFile));
                SQLiteDataAdapter _Adapter = new(_Command);
                DataSet _DataSet = new();
                _Adapter.Fill(_DataSet, TableName);

                if (_DataSet.Tables.Count == 1)
                {
                    var _DataTable = _DataSet.Tables[0];

                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        Dictionary<int, string> _item = new() { { (int)Row["id"], (string)Row["Title"] } };
                        _List.Add(_item);
                    }

                    _Command.Dispose();
                    _Adapter.Dispose();
                    _DataSet.Dispose();

                    return _List;
                }
                else
                {
                    return _List;
                }

            }
            catch (Exception)
            {

                return _List;
            }
        }
        public static int GetMaxID(string DBFile, string _Table) 
        {
            DataTable _DataTable = GetDataTable(DBFile, _Table);
            int _MaxID = (int)_DataTable.Compute("MAX(ID)", "");
            if (_MaxID == 0) { return 1; } else { _MaxID++; }
            _DataTable.Dispose();
            return _MaxID;
        
        }
    }
}
