using CsvParser.Models.Common;

namespace CsvParser.Models.ZenMoney;

public class Transaction: IZenmoneyEntity
{
    //date;categoryName;payee;comment;outcomeAccountName;outcome;outcomeCurrencyShortTitle;incomeAccountName;income;incomeCurrencyShortTitle;createdDate;changedDate;qrCode
    public DateTime Date { get; set; }
    public string CategoryName { get; set; }
    public string Payee { get; set; }
    public string Comment { get; set; }
    public string OutcomeAccountName { get; set; }
    public double Outcome { get; set; }
    public Currency OutcomeCurrencyShortTitle { get; set; }
    public string IncomeAccountName { get; set; }
    public double Income { get; set; }
    public Currency IncomeCurrencyShortTitle { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ChangedDate { get; set; }
    public string QrCode { get; set; }
}