using CsvHelper.Configuration;
using CsvParser.Models.CoinKeeper;

namespace CsvParser.Maps.CoinKeeper;

public sealed class CategoryMap: ClassMap<Category>
{
    public CategoryMap()
    {
        Map(m => m.Name);
        Map(m => m.Budget);
        Map(m => m.ReceivedToDate).Name("Received to date");
        Map(m => m.Icon); 
        Map(m => m.Currency);
    }
}