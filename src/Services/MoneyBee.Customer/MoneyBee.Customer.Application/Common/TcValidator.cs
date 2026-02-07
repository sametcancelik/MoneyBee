using System.Linq;

namespace MoneyBee.Customer.Application.Common;

public static class TcValidator
{
	public static bool IsValid(string tc)
	{
		if (string.IsNullOrEmpty(tc) || tc.Length != 11 || tc[0] == '0' || !tc.All(char.IsDigit))
		{
			return false;
		}
		int[] array = tc.Select((char c) => int.Parse(c.ToString())).ToArray();
		int num = array[0] + array[2] + array[4] + array[6] + array[8];
		int num2 = array[1] + array[3] + array[5] + array[7];
		if ((num * 7 - num2) % 10 != array[9])
		{
			return false;
		}
		if (array.Take(10).Sum() % 10 != array[10])
		{
			return false;
		}
		return true;
	}
}
