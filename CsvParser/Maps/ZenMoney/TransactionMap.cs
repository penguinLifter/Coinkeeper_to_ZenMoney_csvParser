using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvParser.Convertors;
using CsvParser.Models.ZenMoney;

namespace CsvParser.Maps.ZenMoney;

public class TransactionMap: ClassMap<Transaction>
{
    public TransactionMap()
    {
        Map(m => m.Date).TypeConverter<DateTimeConverter>();
        Map(m => m.CategoryName);
        Map(m => m.Payee);
        Map(m => m.Comment);
        Map(m => m.OutcomeAccountName);
        Map(m => m.Outcome).TypeConverter<PriceConverter>();
        Map(m => m.OutcomeCurrencyShortTitle);
        Map(m => m.IncomeAccountName);
        Map(m => m.Income).TypeConverter<PriceConverter>();
        Map(m => m.IncomeCurrencyShortTitle);
        Map(m => m.CreatedDate).TypeConverter<DateTimeConverter>()
            .TypeConverterOption.Format("yyyy-MM-dd HH:mm:ss");
        Map(m => m.ChangedDate).TypeConverter<DateTimeConverter>()
            .TypeConverterOption.Format("yyyy-MM-dd HH:mm:ss");
        Map(m => m.QrCode);
    }
}