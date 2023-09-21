namespace ATMSimulation.API.BL;

public interface IUserServices
{
    Task<string> RegisterAsync(RegisterDTO registerDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);
    Task<int> CheckBalanceAsync(string cardNumber);
    Task<string> CashWithdrawalAsync(string cardNumber, int cash);
    string GenerateJwtToken(string cardNumber);
}
