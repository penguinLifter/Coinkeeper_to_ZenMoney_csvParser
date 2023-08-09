using CsvParser.Models.Common;

namespace CsvParser.Models.CoinKeeper;

public class Transaction
{
    public string Data { get; set; }
    public string Type { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Tags { get; set; }
    public string Amount { get; set; }
    public Currency Currency { get; set; }
    public string AmountConverted { get; set; }
    public string CurrencyOfConversion { get; set; }
    public string Recurrence { get; set; }
    public string Note { get; set; }
}