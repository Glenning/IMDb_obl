using IMDb_obl.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb_obl
{
    public class NormalInsert : IInsert
    {
        public NormalInsert() { }

        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction) //need to do the whole sqlconn
        {
            foreach (Title title in titles)
            {
                string SQL = "INSERT INTO [dbo].[Titles]([tconst]," +
                    "[primaryTitle]," +
                    "[originalTitle]," +
                    "[isAdult]," +
                    "[startYear]," +
                    "[endYear]," +
                    "[runtimeMinutes])" +
                    "VALUES('" + title.Tconst + "', " +
                    "'" + title.PrimaryTitle.Replace("'", "''") + "', " + //replaces entries with single quote with double single quote (french grammar y'know)
                    "'" + title.OriginalTitle.Replace("'", "''") + "', " +
                    "'" + title.IsAdult + "'" +
                    "'" + CheckIntForNull(title.StartYear) + "'" +
                    "'" + CheckIntForNull(title.EndYear) + "'" +
                    "'" + CheckIntForNull(title.RuntimeMinutes) + "')";

                SqlCommand sqlComm = new SqlCommand(SQL, sqlConn);
                sqlComm.ExecuteNonQuery();
            }
        }

        public string CheckIntForNull(int? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            return value.ToString();
        }
    }
}
