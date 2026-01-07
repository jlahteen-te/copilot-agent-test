// C# Console App that validates Finnish SSN
namespace FiSsnValidator;

public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run <Finnish SSN>");
            Console.WriteLine("Example: dotnet run 131052-308T");
            return;
        }

        string ssn = args[0];
        bool isValid = ValidateFinnishSSN(ssn);
        Console.WriteLine(isValid);
    }

    public static bool ValidateFinnishSSN(string ssn)
    {
        if (string.IsNullOrEmpty(ssn) || ssn.Length != 11)
            return false;

        // Check format: DDMMYY-NNNN
        string datePart = ssn.Substring(0, 6);
        char centuryChar = ssn[6];
        string individualNumber = ssn.Substring(7, 3);
        char checkChar = ssn[10];

        // Validate century character
        if (centuryChar != '-' && centuryChar != '+' && centuryChar != 'A')
            return false;

        // Validate date part and individual number are digits
        if (!datePart.All(char.IsDigit) || !individualNumber.All(char.IsDigit))
            return false;

        // Validate date
        int day = int.Parse(datePart.Substring(0, 2));
        int month = int.Parse(datePart.Substring(2, 2));
        int year = int.Parse(datePart.Substring(4, 2));

        // Determine century
        int fullYear = centuryChar switch
        {
            '+' => 1800 + year,
            '-' => 1900 + year,
            'A' => 2000 + year,
            _ => 0
        };

        // Validate date is valid
        try
        {
            new DateTime(fullYear, month, day);
        }
        catch
        {
            return false;
        }

        // Validate check character
        string checkString = datePart + individualNumber;
        long checkValue = long.Parse(checkString);
        long remainder = checkValue % 31;
        
        string checkChars = "0123456789ABCDEFHJKLMNPRSTUVWXY";
        char expectedCheckChar = checkChars[(int)remainder];

        return checkChar == expectedCheckChar;
    }
}
