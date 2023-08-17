using CsvParser.Models.CoinKeeper;
using CsvParser.Service;

namespace CsvParser.Tests;

public class CoinKeeperServiceTests
{
    private CoinKeeperService _service;
    private string path = @"E:\Repo\CoinKeeper_to_ZenMoney\CsvParser.Tests\Data\export.csv";
    [SetUp]
    public void Setup()
    {
        _service = new();
    }

    [Test]
    public void Read_all_lines_of_code()
    {
        var x = _service.ReadCsv(path);
        var count = x.Sum(pairs => pairs.Value.Count);
        var expected = 1342 - 12;
        Assert.That(count, Is.EqualTo(expected));
    }
    
    [Test]
    public void IncomeCategories_returns_expected_count()
    {
        var incomes = _service.ReadCsv(path)["IncomeCategories"];
        var expected = 4;
        Assert.That(incomes.Count, Is.EqualTo(expected));
    }
    
    [Test]
    public void ExpensesCategories_returns_expected_count()
    {
        var incomes = _service.ReadCsv(path)["ExpensesCategories"];
        var expected = 22;
        Assert.That(incomes.Count, Is.EqualTo(expected));
    }
    [Test]
    public void Account_returns_expected_count()
    {
        var incomes = _service.ReadCsv(path)[nameof(Account)];
        var expected = 19;
        Assert.That(incomes.Count, Is.EqualTo(expected));
    }
    [Test]
    public void SubCategory_returns_expected_count()
    {
        var incomes = _service.ReadCsv(path)[nameof(SubCategory)];
        var expected = 17;
        Assert.That(incomes.Count, Is.EqualTo(expected));
    }
    
}