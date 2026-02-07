namespace MoneyBee.Shared.Constants;

public static class GlobalConstants
{
	public static class BusinessRules
	{
		public const decimal DailyTransferLimit = 10000m;

		public const decimal ManualApprovalThreshold = 1000m;

		public const int ManualApprovalDelayMinutes = 5;

		public const int MinimumAgeLimit = 18;
	}

	public static class Rates
	{
		public const int AuthRateLimitPerMinute = 100;
	}
}
