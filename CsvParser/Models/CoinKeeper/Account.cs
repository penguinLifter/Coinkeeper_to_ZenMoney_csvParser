using CsvParser.Models.Common;

namespace CsvParser.Models.CoinKeeper;

public class Account : ICoinKeeperEntity
{
    public string Name { get; init; }
    public double CurrentAmount { get; init; }
    public string Icon { get; init; }
    public Currency Currency { get; init; }
}