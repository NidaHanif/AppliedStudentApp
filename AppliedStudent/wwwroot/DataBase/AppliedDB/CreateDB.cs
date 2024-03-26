using System.Data.SQLite;
using System.Data;
using static AppliedDB.SQLiteDB;

namespace AppliedDB
{
    public class CreateDB
    {
        public SQLiteConnection UsersConnection { get; set; }
        public SQLiteConnection AppConnection { get; set; }
        public string User { get; set; } = string.Empty;


        public CreateDB()
        {
            UsersConnection = AppliedConnection.GetUsersConnection();
            if (UsersConnection.State != ConnectionState.Open)
            { UsersConnection.Open(); }


            if (User != null)
            {
                UsersConnection = AppliedConnection.GetConnection(User);
                if (UsersConnection.State != ConnectionState.Open)
                { UsersConnection.Open(); }
            }

            AppConnection = new();
        }

        public CreateDB(string _User)
        {
            UsersConnection = AppliedConnection.GetConnection(_User);
            if (UsersConnection.State != ConnectionState.Open)
            { UsersConnection.Open(); }

            AppConnection = new();

        }
        

    }
}
