namespace TimeApp.Tests;

public class FinnishSSNValidationTests
{
    [Fact]
    public void ValidateFinnishSSN_ValidSSN_ReturnsTrue()
    {
        // Valid SSN: 131052-308T
        bool result = Program.ValidateFinnishSSN("131052-308T");
        Assert.True(result);
    }

    [Fact]
    public void ValidateFinnishSSN_ValidSSNWith1900s_ReturnsTrue()
    {
        // Valid SSN from 1900s with - separator
        bool result = Program.ValidateFinnishSSN("010190-123M");
        Assert.True(result);
    }

    [Fact]
    public void ValidateFinnishSSN_ValidSSNWith2000s_ReturnsTrue()
    {
        // Valid SSN from 2000s with A separator
        bool result = Program.ValidateFinnishSSN("010101A123N");
        Assert.True(result);
    }

    [Fact]
    public void ValidateFinnishSSN_InvalidCheckCharacter_ReturnsFalse()
    {
        // Invalid check character (should be T, not X)
        bool result = Program.ValidateFinnishSSN("131052-308X");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_InvalidLength_ReturnsFalse()
    {
        // SSN too short
        bool result = Program.ValidateFinnishSSN("131052-308");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_NullSSN_ReturnsFalse()
    {
        // Null SSN
        bool result = Program.ValidateFinnishSSN(null);
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_EmptySSN_ReturnsFalse()
    {
        // Empty SSN
        bool result = Program.ValidateFinnishSSN("");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_InvalidCenturyCharacter_ReturnsFalse()
    {
        // Invalid century character (should be -, +, or A)
        bool result = Program.ValidateFinnishSSN("131052B308T");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_InvalidDate_ReturnsFalse()
    {
        // Invalid date (February 31st doesn't exist)
        bool result = Program.ValidateFinnishSSN("310290-123A");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_NonNumericDatePart_ReturnsFalse()
    {
        // Non-numeric characters in date part
        bool result = Program.ValidateFinnishSSN("13AB52-308T");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_NonNumericIndividualNumber_ReturnsFalse()
    {
        // Non-numeric characters in individual number
        bool result = Program.ValidateFinnishSSN("131052-3A8T");
        Assert.False(result);
    }

    [Fact]
    public void ValidateFinnishSSN_ValidSSNWith1800s_ReturnsTrue()
    {
        // Valid SSN from 1800s with + separator
        bool result = Program.ValidateFinnishSSN("010150+123A");
        Assert.True(result);
    }
}
