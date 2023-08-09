using CsvHelper.Configuration;
using CsvParser.Models.CoinKeeper;

namespace CsvParser.Maps.CoinKeeper;

public sealed class TransactionMap: ClassMap<Transaction>
{
    public TransactionMap()
    {
        Map(m => m.Data);
        Map(m => m.Type);
        Map(m => m.From);
        Map(m => m.To); 
        Map(m => m.Tags);
        Map(m => m.Amount);
        Map(m => m.Currency);
        Map(m => m.AmountConverted).Name("Amount converted");
        Map(m => m.CurrencyOfConversion).Name("Currency of conversion");
        Map(m => m.Recurrence);
        Map(m => m.Note);
    }
}