using IMDb_obl.Models;
using Microsoft.Data.SqlClient;

namespace IMDb_obl
{
    public interface IInsert
    {
        string CheckIntForNull(int? value);
        void Insert(List<Title> titles, SqlConnection sqlConn);
    }
}