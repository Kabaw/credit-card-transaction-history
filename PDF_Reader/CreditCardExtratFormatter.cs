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
        DateTime date = DateTime.Now;
        string type = "";
        string company = "";
        decimal value = 0;

        //line = $"{date}; {type}; {company}; {value}";

        stringBuilder.AppendLine(line);
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

        if (!int.TryParse(leftSection[0], out num))
            return false;

        return true;
    }

    private static bool EvaluateMonthShort(string text)
    {
        foreach (string monthShort in Enum.GetNames(typeof(MonthShort)))
            if (text == monthShort)
                return true;

        return false;
    }
}