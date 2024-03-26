using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliedDB
{
    public static class Commands
    {
        public static SQLiteCommand Insert(DataRow CurrentRow, string DBFile)
        {
            DataColumnCollection _Columns = CurrentRow.Table.Columns;
            SQLiteCommand _Command = new(SQLiteDB.AppliedConnection.GetConnection(DBFile));

            StringBuilder _CommandString = new();
            string _LastColumn = _Columns[_Columns.Count - 1].ColumnName.ToString();
            string _TableName = CurrentRow.Table.TableName;
            string _ParameterName;

            _CommandString.Append("INSERT INTO [");
            _CommandString.Append(_TableName);
            _CommandString.Append("] VALUES (");

            foreach (DataColumn _Column in _Columns)
            {
                string _ColumnName = _Column.ColumnName.ToString();
                _CommandString.Append(string.Concat('@', _Column.ColumnName));
                if (_ColumnName != _LastColumn)
                { _CommandString.Append(','); }
                else
                { _CommandString.Append(") "); }
            }

            _Command.CommandText = _CommandString.ToString();

            foreach (DataColumn _Column in _Columns)
            {
                if (_Column == null) { continue; }
                _ParameterName = string.Concat('@', _Column.ColumnName.Replace(" ", ""));
                _Command.Parameters.AddWithValue(_ParameterName, CurrentRow[_Column.ColumnName]);
            }

            CurrentRow["ID"] = DataSource.GetMaxID(DBFile, _TableName);
            _Command.Parameters["@ID"].Value = CurrentRow["ID"];
            return _Command;
        }
        public static SQLiteCommand UpDate(DataRow CurrentRow, string DBFile)
        {

            var _TableName = CurrentRow.Table.TableName;
            var _Columns = CurrentRow.Table.Columns;
            var _Command = new SQLiteCommand(SQLiteDB.AppliedConnection.GetConnection(DBFile));
            var _CommandString = new StringBuilder();
            var _LastColumn = _Columns[_Columns.Count - 1].ColumnName.ToString();

            _CommandString.Append(string.Concat("UPDATE [", _TableName, "] SET "));

            foreach (DataColumn _Column in _Columns)
            {
                if (_Column.ColumnName == "ID") { continue; }
                _CommandString.Append(string.Concat("[", _Column.ColumnName, "]"));
                _CommandString.Append('=');
                _CommandString.Append(string.Concat('@', _Column.ColumnName.Replace(" ", "")));

                if (_Column.ColumnName == _LastColumn)
                {
                    _CommandString.Append(string.Concat(" WHERE ID = @ID"));
                }
                else
                {
                    _CommandString.Append(',');
                }
            }

            _Command.CommandText = _CommandString.ToString();

            foreach (DataColumn _Column in _Columns)
            {
                if (_Column == null) { continue; }
                var _ParameterName = string.Concat('@', _Column.ColumnName.Replace(" ", ""));
                _Command.Parameters.AddWithValue(_ParameterName, CurrentRow[_Column.ColumnName]);
            }

            return _Command;
        }
        public static SQLiteCommand Delete(DataRow CurrentRow, string DBFile)
        {
            var _TableName = CurrentRow.Table.TableName;
            var _Command = new SQLiteCommand(SQLiteDB.AppliedConnection.GetConnection(DBFile));
            _Command.Parameters.AddWithValue("@ID", CurrentRow["ID"]);
            _Command.CommandText = $"DELETE FROM [{_TableName}] WHERE ID=@ID";
            return _Command;
        }

    }
}
