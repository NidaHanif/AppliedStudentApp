using System.Data.SQLite;
using System.Data;

namespace AppliedStudent.Codes;
public class ConnectionClass
{
    public static SQLiteConnection GetConnected()
    {
        var FileName = "C:\\Development\\Students\\AppliedStudentApp\\AppliedStudent\\wwwroot\\DataBase\\Students.db";
        var ConnectionString = $"Data Source={FileName}";
        var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}