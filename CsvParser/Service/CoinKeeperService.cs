using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvParser.Maps.CoinKeeper;
using CsvParser.Models.CoinKeeper;

namespace CsvParser.Service;

public class CoinKeeperService: ICsvReader
{
    private HeaderType headerType;
    private List<Account?> _accounts = new ();
    private List<Category?> _categories = new ();
    private List<SubCategory?> _subCategories = new ();
    private List<Transaction?> _transactions = new ();
    public List<T> ReadCsv<T>(string path)
    {
        using var reader = new StreamReader(Path.Combine(path));

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            Delimiter = ",",
            MissingFieldFound = null
        };
        using var csv = new CsvReader(reader, config);
        
        csv.Context.RegisterClassMap<AccountMap>();
        csv.Context.RegisterClassMap<CategoryMap>();
        csv.Context.RegisterClassMap<SubCategoryMap>();
        csv.Context.RegisterClassMap<TransactionMap>();
        
        while (csv.Read())
        {
            if (csv.GetField(0) == csv.Context.Maps.Find<Transaction>().MemberMaps[0].Data.Member.Name)
            {
                headerType = HeaderType.Transactions;
                csv.ReadHeader();
            }
            else if (csv.GetField(0) == "Name" && csv.GetField(1) == "Budget")
            {
                headerType = HeaderType.Categories;
                csv.ReadHeader();
            }
            else if (csv.GetField(0) == "Name" && csv.GetField(1) == "Current amount")
            {
                headerType = HeaderType.Accounts;
                csv.ReadHeader();
                continue;
            }
            else if (csv.GetField(0) == "Name" && csv.GetField(1) == "Received to date")
            {
                headerType = HeaderType.SubCategory;
                csv.ReadHeader();
            }

            AddEntity(headerType, csv);
        }
        return null;
    }
    
    private void AddEntity(HeaderType header, IReaderRow csv)
    {
        switch (header)
        {
            case HeaderType.Accounts:
                _accounts.Add(csv.GetRecord<Account>());
                break;
            case HeaderType.Categories:
                _categories.Add(csv.GetRecord<Category>());
                break;

            case HeaderType.Transactions:
                _transactions.Add(csv.GetRecord<Transaction>());
                break;
            case HeaderType.SubCategory:
                _subCategories.Add(csv.GetRecord<SubCategory>());
                break;
        }
    }
}