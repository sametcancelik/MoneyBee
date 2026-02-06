namespace MoneyBee.Customer.Application.Common;

public static class TcValidator
{
    public static bool IsValid(string tc)
    {
        if (string.IsNullOrEmpty(tc) || tc.Length != 11 || tc[0] == '0' || !tc.All(char.IsDigit)) 
            return false;

        int[] digits = tc.Select(c => int.Parse(c.ToString())).ToArray();
        int sumOdd = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int sumEven = digits[1] + digits[3] + digits[5] + digits[7];

        if ((sumOdd * 7 - sumEven) % 10 != digits[9]) return false;
        if ((digits.Take(10).Sum()) % 10 != digits[10]) return false;

        return true;
    }
}
