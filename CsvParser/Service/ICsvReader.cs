namespace CsvParser.Service;

public interface ICsvReader
{
    public List<T> ReadCsv<T>(string path);
}