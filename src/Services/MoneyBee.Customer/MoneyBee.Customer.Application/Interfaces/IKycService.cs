namespace MoneyBee.Customer.Application.Interfaces;

public interface IKycService
{
    bool ValidateNationalId(string nationalId);

    Task<bool> VerifyWithExternalServiceAsync(Guid userId, string nationalId, string firstName, string lastName, int birthYear);
}