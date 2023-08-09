using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvParser.Convertors;

public class PriceConverter: DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text.Length > 5 && text[5] == ' ')
        {
            text = text.Remove(5, 1).Insert(5, "0");
        }
        return double.Parse(text);
    }

    public override string? ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value.ToString();
    }
}