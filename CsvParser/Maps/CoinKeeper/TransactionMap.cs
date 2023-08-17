using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvParser.Convertors;
using CsvParser.Models.CoinKeeper;
using CsvParser.Models.Common;

namespace CsvParser.Maps.CoinKeeper;

public sealed class TransactionMap: ClassMap<Transaction>
{
    public TransactionMap()
    {
        Map(m => m.Data)
            .TypeConverter<DateTimeConverter>();
        Map(m => m.Type)
            .TypeConverter(new EnumConverter(typeof(CoinKeeperType)));
        Map(m => m.From);
        Map(m => m.To); 
        Map(m => m.Tags);
        Map(m => m.Amount)
            .TypeConverter<PriceConverter>();
        Map(m => m.Currency)
            .TypeConverter(new EnumConverter(typeof(Currency)));
        Map(m => m.AmountConverted)
            .Name("Amount converted")
            .TypeConverter<PriceConverter>();
        Map(m => m.CurrencyOfConversion)
            .Name("Currency of conversion")
            .TypeConverter(new EnumConverter(typeof(Currency)));
        Map(m => m.Recurrence);
        Map(m => m.Note);
    }
}