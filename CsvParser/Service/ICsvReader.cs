using CsvParser.Models.CoinKeeper;

namespace CsvParser.Service;

public interface ICsvReader
{
    public Dictionary<string, List<ICoinKeeperEntity>> ReadCsv(string path);
}