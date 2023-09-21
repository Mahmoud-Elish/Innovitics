namespace ATMSimulation.API.BL;

public record TokenDTO
{
    public string Token { get; init; }
    public DateTime ExpiryDate { get; init; }
}
