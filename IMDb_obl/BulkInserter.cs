using IMDb_obl.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb_obl
{
    public class BulkInserter : IInsert
    {
        public string CheckIntForNull(int? value)
        {
            throw new NotImplementedException();
        }

        public void Insert(List<Title> titles, SqlConnection sqlConn, SqlTransaction transAction)
        {
            DataTable titleTable = new DataTable();

            DataColumn tconstCol = new DataColumn("TConst", typeof(string));
            DataColumn primaryTitleCol = new DataColumn("PrimaryTitle", typeof(string));
            DataColumn originalTitleCol = new DataColumn("OriginalTitle", typeof(string));
            DataColumn isAdultCol = new DataColumn("IsAdult", typeof(bool));
            DataColumn startYearCol = new DataColumn("StartYear", typeof(int));
            DataColumn endYearCol = new DataColumn("EndYear", typeof(int));
            DataColumn runtimeMinutesCol = new DataColumn("RuntimeMinutes", typeof(int));

            titleTable.Columns.Add(tconstCol);
            titleTable.Columns.Add(primaryTitleCol);
            titleTable.Columns.Add(originalTitleCol);
            titleTable.Columns.Add(startYearCol);
            titleTable.Columns.Add(endYearCol);
            titleTable.Columns.Add(runtimeMinutesCol);

            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                FillParameter(titleRow, "TConst", title.Tconst);
                FillParameter(titleRow, "PrimaryTitle", title.PrimaryTitle);
                FillParameter(titleRow, "OriginalTitle", title.OriginalTitle);
                FillParameter(titleRow, "IsAdult", title.IsAdult);
                FillParameter(titleRow, "StartYear", title.StartYear);
                FillParameter(titleRow, "EndYear", title.EndYear);
                FillParameter(titleRow, "RuntimeMinutes", title.RuntimeMinutes);
                titleTable.Rows.Add(titleRow);
            }
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepNulls, transAction);
            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.WriteToServer(titleTable);
        }

        public void FillParameter(DataRow row, string columnName, object? value)
        {
            if (value != null) 
            {
                row[columnName] = value;
            }
            else
            {
                row[columnName] = DBNull.Value;
            }
        }
    }
}
