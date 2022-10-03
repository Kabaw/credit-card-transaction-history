using System.Text;

namespace PDF_Reader;

public enum MonthShort
{
    JAN = 1,
    FEV = 2,
    MAR = 3,
    ABR = 4,
    MAI = 5,
    JUN = 6,
    JUL = 7,
    AGO = 8,
    SET = 9,
    OUT = 10,
    NOV = 11,
    DEZ = 12
}


public static class CreditCardExtratFormatter
{
    public static string FormatAsCSV(string extract)
    {
        var stringReader = new StringReader(extract);
        var stringBuilder = new StringBuilder();

        while (true)
        {
            var line = stringReader.ReadLine();

            if (line is null)
                break;

            line = line.Trim();

            if (EvaluateTransactionLine(line))
                FormatTransaction(line, stringBuilder);
        }

        return stringBuilder.ToString();
    }

    private static void FormatTransaction(string line, StringBuilder stringBuilder)
    {
        var date = RetriveDate(line);
        var (business, value) = RetriveBusinessAndValue(line);

        line = $"{date}; {business}; {value}";

        stringBuilder.AppendLine(line);
    }

    private static string RetriveDate(string line)
    {
        var day = line.Substring(0, 2);
        var month = line.Substring(3, 3);

        return new DateTime(DateTime.Today.Year, (int)GetMonthShortByName(month)!, int.Parse(day)).ToString("dd/MM/yyyy");
    }

    private static (string, string) RetriveBusinessAndValue(string line)
    {
        line = line.Substring(6).Trim();

        var startIndex = line.Length - 1;

        while (line.Substring(startIndex, 1) != " ")
            startIndex--;

        var business = line.Substring(0, startIndex).Trim();
        var value = line.Substring(startIndex, line.Length - startIndex).Trim();

        return (business, value);
    }

    private static bool EvaluateTransactionLine(string line)
    {
        if (line.Length < 15)
            return false;

        string[] leftSection = line.Substring(0, 6).Split(" ");

        if(leftSection.Length != 2 ||
           !int.TryParse(leftSection[0], out int num) ||
           !EvaluateMonthShort(leftSection[1]))
            return false;

        string rightSection = line.Substring(line.Length - 2);

        if (!int.TryParse(rightSection, out num))
            return false;

        return true;
    }

    private static bool EvaluateMonthShort(string text)
    {
        return GetMonthShortByName(text) is null ? false: true;
    }

    private static MonthShort? GetMonthShortByName(string monthShortName)
    {
        foreach (MonthShort monthShort in Enum.GetValues(typeof(MonthShort)))
            if (monthShortName == monthShort.ToString())
                return monthShort;

        return null;
    }
}