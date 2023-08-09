using CsvHelper.Configuration;
using CsvParser.Models.CoinKeeper;

namespace CsvParser.Maps.CoinKeeper;

public sealed class SubCategoryMap: ClassMap<SubCategory>
{
    public SubCategoryMap()
    {
        Map(m => m.Name);
        Map(m => m.ReceivedToDate).Name("Received to date");
    }
}