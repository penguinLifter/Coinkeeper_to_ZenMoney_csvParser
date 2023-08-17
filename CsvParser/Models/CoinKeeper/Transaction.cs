using CsvParser.Models.Common;

namespace CsvParser.Models.CoinKeeper;

public class Transaction : ICoinKeeperEntity
{
    public DateTime Data { get; set; }
    public CoinKeeperType Type { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Tags { get; set; }
    public double Amount { get; set; }
    public Currency Currency { get; set; }
    public double AmountConverted { get; set; }
    public Currency CurrencyOfConversion { get; set; }
    public string Recurrence { get; set; }
    public string Note { get; set; }
}

public enum CoinKeeperType
{
    Expense,
    Transfer
}