using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvParser.Maps.CoinKeeper;
using CsvParser.Models.CoinKeeper;
using Transaction = CsvParser.Models.CoinKeeper.Transaction;

namespace CsvParser.Service;

public class CoinKeeperService: ICsvReader
{
    private HeaderType headerType;
    private List<ICoinKeeperEntity?> _accounts = new ();
    private List<ICoinKeeperEntity?> _incomeCategories = new ();
    private List<ICoinKeeperEntity?> _expensesCategories = new ();
    private List<ICoinKeeperEntity?> _subCategories = new ();
    private List<ICoinKeeperEntity?> _transactions = new() { new Transaction() };
    public Dictionary<string, List<ICoinKeeperEntity>> ReadCsv(string path)
    {
        using var reader = new StreamReader(Path.Combine(path));

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            Delimiter = ",",
            MissingFieldFound = null
        };
        using var csv = new CsvReader(reader, config);
        var isIncome = false;
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
                continue;
            }
            else if (csv.GetField(0) == "Name" && csv.GetField(1) == "Budget")
            {
                if (!isIncome)
                {
                    headerType = HeaderType.Income;
                    csv.ReadHeader();
                    isIncome = true;
                    continue;
                }
                else
                {
                    headerType = HeaderType.Expense;
                    csv.ReadHeader();
                    isIncome = false;
                    continue;
                }
                
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
                continue;
            }

            AddEntity(headerType, csv);
        }
        return new Dictionary<string, List<ICoinKeeperEntity>>
        {
            {nameof(Account), _accounts},
            {"IncomeCategories", _incomeCategories},
            {"ExpensesCategories", _expensesCategories},
            {nameof(SubCategory), _subCategories},
            {nameof(Transaction), _transactions}
        };
    }

    public void Convert(Dictionary<string, List<ICoinKeeperEntity>> transactions)
    {
        var list = (from Transaction? t in transactions[nameof(Transaction)] select Parser(t)).ToList();
    }

    private Models.ZenMoney.Transaction Parser(Transaction transaction)
    {
        var zenMoneyTransaction = new Models.ZenMoney.Transaction
        {
            Date = transaction.Data,
            Payee = transaction.Tags,
            Comment = transaction.Note,
            CreatedDate = transaction.Data,
            ChangedDate = transaction.Data,
            OutcomeCurrencyShortTitle = transaction.Currency,
            IncomeCurrencyShortTitle = transaction.CurrencyOfConversion,
        };
        
        if (transaction.Type == CoinKeeperType.Expense)
        {
            zenMoneyTransaction.OutcomeAccountName = transaction.From;
            zenMoneyTransaction.IncomeAccountName = transaction.From;
            zenMoneyTransaction.CategoryName = transaction.To;
            zenMoneyTransaction.Outcome = transaction.Amount;
            zenMoneyTransaction.Income = 0;
        }
        if (transaction.Type == CoinKeeperType.Transfer)
        {
            //тут може бути і трансфер і інкам
            
            zenMoneyTransaction.OutcomeAccountName = transaction.From;
            zenMoneyTransaction.IncomeAccountName = transaction.From;
            zenMoneyTransaction.CategoryName = transaction.To;
            zenMoneyTransaction.Outcome = transaction.Amount;
            zenMoneyTransaction.Income = transaction.Amount;
        }

        return zenMoneyTransaction;
    }

    public void WriteToFile(List<Models.ZenMoney.Transaction> transactions, string path)
    {
        
        using (var writer = new StreamWriter(path))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<Maps.ZenMoney.TransactionMap>();
            csv.WriteRecords(transactions);
        }
    }

    private void AddEntity(HeaderType header, IReaderRow csv)
    {
        switch (header)
        {
            case HeaderType.Accounts:
                _accounts.Add(csv.GetRecord<Account>());
                break;
            case HeaderType.Expense:
                _expensesCategories.Add(csv.GetRecord<Category>());
                break;
            case HeaderType.Income:
                _incomeCategories.Add(csv.GetRecord<Category>());
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