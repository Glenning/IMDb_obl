using IMDb_obl.Models;

int lineCount = 0;

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
    string tconst = splitLine[0];
    string primaryTitle = splitLine[2];
    string originalTitle = splitLine[3];
    bool isAdult = splitLine[4] == "1";
    /*int startYear = splitLine[5];
    int endYear = splitLine[6];
    int runtimeMinutes = splitLine[7];*/

}