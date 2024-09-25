using IMDb_obl.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IMDb_obl
{
    public class PreparedInserter : IInsert
    {
        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction)
        {
            string SQL = "INSERT INTO [dbo].[Titles]([tconst]," +
                    "[primaryTitle]," +
                    "[originalTitle]," +
                    "[isAdult]," +
                    "[startYear]," +
                    "[endYear]," +
                    "[runtimeMinutes])" +
                    "VALUES(@Tconst" +
                    ", @PrimaryTitle" +
                    ", @OriginalTitle" +
                    ", @IsAdult" +
                    ", @StartYear"+
                    ", @EndYear" +
                    ", @RuntimeMinutes";

            SqlCommand sqlComm = new SqlCommand(SQL, sqlConn);
            sqlComm.Prepare();

            SqlParameter tconstPar = new SqlParameter(@"Tconst", SqlDbType.VarChar, 50);
            sqlComm.Parameters.Add(tconstPar);
            SqlParameter primaryTitlePar = new SqlParameter("@PrimaryTitle", SqlDbType.VarChar, 200);
            sqlComm.Parameters.Add(primaryTitlePar);
            SqlParameter originalTitlePar = new SqlParameter("@OriginalTitle", SqlDbType.VarChar, 200);
            sqlComm.Parameters.Add(originalTitlePar);
            SqlParameter isAdultPar = new SqlParameter("@IsAdult", SqlDbType.Bit);
            sqlComm.Parameters.Add(isAdultPar);
            SqlParameter startYearPar = new SqlParameter("@StartYear", SqlDbType.Int);
            sqlComm.Parameters.Add(startYearPar);
            SqlParameter endYearPar = new SqlParameter("@EndYear", SqlDbType.Int);
            sqlComm.Parameters.Add(endYearPar);
            SqlParameter runtimeMinutesPar = new SqlParameter("@RuntimeMinutes", SqlDbType.Int);
            sqlComm.Parameters.Add(runtimeMinutesPar);

            foreach (Title title in titles)
            {
                tconstPar.Value = title.Tconst;
                primaryTitlePar.Value = title.PrimaryTitle;
                originalTitlePar.Value = CheckIntForNull(title.OriginalTitle);
                isAdultPar.Value = title.IsAdult;
                startYearPar.Value = CheckIntForNull(title.StartYear);
                endYearPar.Value = CheckIntForNull(title.EndYear);
                runtimeMinutesPar.Value = CheckIntForNull(title.RuntimeMinutes);

                sqlComm.ExecuteNonQuery();
            }
        }
        public Object CheckIntForNull(Object? value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }

        string IInsert.CheckIntForNull(int? value)
        {
            throw new NotImplementedException();
        }
    }
}
