using IMDb_obl;
using IMDb_obl.Models;
using Microsoft.Data.SqlClient;

int lineCount = 0;

List<Title> titles = new List<Title>();
string filePath = "C:/title.basics.tsv";
foreach (string line in File.ReadLines(filePath).Skip(1)) //skips first line
{
    Console.WriteLine(line);
    if (lineCount++ == 50000)
    {
        break;
    }
    string[] splitLine = line.Split('\t');
    if (splitLine.Length != 9)
    {
        throw new Exception("Wrong tab amount! " + line); //if dataset has more tabs than allowed, throw exception + line where error occured
    }
    //splitlines need to be made for every value
    string tconst = splitLine[0];
    string primaryTitle = splitLine[2];
    string originalTitle = splitLine[3];
    bool isAdult = splitLine[4] == "1"; //cannot be null
    int? startYear = ParseInt(splitLine[5]);
    int? endYear = ParseInt(splitLine[6]);
    int? runtimeMinutes = ParseInt(splitLine[7]);

    Title newTitle = new Title() { Tconst = tconst, 
        PrimaryTitle = primaryTitle, 
        OriginalTitle = originalTitle, 
        IsAdult = isAdult, 
        StartYear = startYear, 
        EndYear = endYear, 
        RuntimeMinutes = runtimeMinutes};

    titles.Add(newTitle);

    lineCount++;
}

Console.WriteLine($"Title amount: {titles.Count}");

SqlConnection sqlConn = new SqlConnection("server=localhost;database=IMDb_obl;" +
                                          "user id=sa;" +
                                          "password=dennyepassword;" +
                                          "TrustServerCertificate=false;");
sqlConn.Open();
SqlTransaction transAction = sqlConn.BeginTransaction();

DateTime before = DateTime.Now;
try
{
    NormalInsert inserter = new NormalInsert();
    inserter.Insert(titles, sqlConn);
} catch (SqlException ex)
{
    Console.WriteLine(ex.Message);
    transAction.Rollback();
}

DateTime after = DateTime.Now;

sqlConn.Close();

Console.WriteLine($"Time taken: {after}");

int? ParseInt(string value)
{
    if (value.ToLower() == "\\n") //checks if it is \n
    {
        return null;
    }
    return int.Parse(value);
}