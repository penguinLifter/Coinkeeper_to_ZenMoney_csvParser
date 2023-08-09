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
    public void Test()
    {
        //_service.ReadCsv<>(path);
        Assert.Pass();
    }
    
}