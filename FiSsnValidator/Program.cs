// C# Console App that validates Finnish SSN
namespace FiSsnValidator;

public class Program
{
    private const string FINNISH_SSN_CHECK_CHARACTERS = "0123456789ABCDEFHJKLMNPRSTUVWXY";

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run <Finnish SSN or Business ID>");
            Console.WriteLine("Example: dotnet run 131052-308T");
            Console.WriteLine("Example: dotnet run 2464491-9");
            return;
        }

        string input = args[0];
        
        // Detect if input is a Business ID (format: 7 digits - 1 digit)
        bool isBusinessID = input.Length == 9 && input.Contains('-') && 
                           input.IndexOf('-') == 7;
        
        bool isValid;
        if (isBusinessID)
        {
            isValid = ValidateFinnishBusinessID(input);
        }
        else
        {
            isValid = ValidateFinnishSSN(input);
        }
        
        Console.WriteLine(isValid);
    }

    /// <summary>
    /// Validates a Finnish Social Security Number (SSN).
    /// </summary>
    /// <param name="ssn">The Finnish SSN to validate in format DDMMYY-NNNN where - can be -, +, or A</param>
    /// <returns>True if the SSN is valid, false otherwise</returns>
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
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }

        // Validate check character
        string checkString = datePart + individualNumber;
        long checkValue = long.Parse(checkString);
        long remainder = checkValue % 31;
        
        char expectedCheckChar = FINNISH_SSN_CHECK_CHARACTERS[(int)remainder];

        return checkChar == expectedCheckChar;
    }

    /// <summary>
    /// Validates a Finnish Business ID (Y-tunnus).
    /// </summary>
    /// <param name="businessId">The Finnish Business ID to validate in format NNNNNNN-C</param>
    /// <returns>True if the Business ID is valid, false otherwise</returns>
    public static bool ValidateFinnishBusinessID(string businessId)
    {
        if (string.IsNullOrEmpty(businessId) || businessId.Length != 9)
            return false;

        // Check format: NNNNNNN-C
        if (businessId[7] != '-')
            return false;

        string numberPart = businessId.Substring(0, 7);
        char checkDigit = businessId[8];

        // Validate that number part contains only digits
        if (!numberPart.All(char.IsDigit))
            return false;

        // Validate that check digit is a digit
        if (!char.IsDigit(checkDigit))
            return false;

        // Calculate check digit using modulo 11 algorithm
        int[] multipliers = { 7, 9, 10, 5, 8, 4, 2 };
        int sum = 0;

        for (int i = 0; i < 7; i++)
        {
            sum += (numberPart[i] - '0') * multipliers[i];
        }

        int remainder = sum % 11;
        int expectedCheckDigit;

        if (remainder == 0)
        {
            expectedCheckDigit = 0;
        }
        else if (remainder == 1)
        {
            // Business IDs with remainder 1 are not valid
            return false;
        }
        else
        {
            expectedCheckDigit = 11 - remainder;
        }

        return (checkDigit - '0') == expectedCheckDigit;
    }
}
