using CsvParser.Models.Common;

namespace CsvParser.Models.CoinKeeper;

public class Category : ICoinKeeperEntity
{
    public string Name { get; set; }
    public string Budget { get; set; }
    public string ReceivedToDate { get; set; }
    public string Icon { get; set; }
    public Currency Currency { get; set; }
}