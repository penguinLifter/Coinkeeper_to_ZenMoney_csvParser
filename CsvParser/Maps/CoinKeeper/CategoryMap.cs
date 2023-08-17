using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvParser.Models.CoinKeeper;
using CsvParser.Models.Common;

namespace CsvParser.Maps.CoinKeeper;

public sealed class CategoryMap: ClassMap<Category>
{
    public CategoryMap()
    {
        Map(m => m.Name);
        Map(m => m.Budget);
        Map(m => m.ReceivedToDate).Name("Received to date");
        Map(m => m.Icon); 
        Map(m => m.Currency).TypeConverter(new EnumConverter(typeof(Currency)));
    }
}