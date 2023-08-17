using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvParser.Convertors;
using CsvParser.Models.CoinKeeper;
using CsvParser.Models.Common;

namespace CsvParser.Maps.CoinKeeper;

public sealed class AccountMap: ClassMap<Account>
{
    public AccountMap()
    {
        Map(m => m.Name);
        Map(m => m.CurrentAmount).Name("Current amount").TypeConverter<PriceConverter>();
        Map(m => m.Icon);
        Map(m => m.Currency).TypeConverter(new EnumConverter(typeof(Currency)));
    }
}