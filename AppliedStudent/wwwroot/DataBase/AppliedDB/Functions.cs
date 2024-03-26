using System.Data;

namespace AppliedDB
{
    public class Functions
    {

        public static bool Seek(DataRow CurrentRow, string DBFile, int ID)
        {
            var SQLQuery = $"SELECT [ID] FROM {CurrentRow.Table.TableName} WHERE [ID]={ID}";
            DataTable _Table = DataSource.GetDataTable(DBFile, SQLQuery, CurrentRow.Table.TableName);
            if(_Table != null) { return false; }
            if(_Table.Rows.Count == 0) { return false; }
            return true;
        }
        public static DataRow RemoveNull(DataRow CurrentRow) 
        {
            foreach (DataColumn Column in CurrentRow.Table.Columns)
            {
                if(CurrentRow[Column] == null)
                {
                    var _Type = Column.GetType();

                    if(_Type == typeof(string)) { CurrentRow[Column] = ""; }
                    if(_Type == typeof(int)) { CurrentRow[Column] = 0; }
                    if(_Type == typeof(long)) { CurrentRow[Column] = 0; }
                    if(_Type == typeof(short)) { CurrentRow[Column] = 0; }
                    if(_Type == typeof(decimal)) { CurrentRow[Column] = 0.00M; }
                    if(_Type == typeof(DateTime)) { CurrentRow[Column] = DateTime.Now; }
                }
            }
            return CurrentRow;
        }


    }
}
