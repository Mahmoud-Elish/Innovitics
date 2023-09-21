namespace ATMSimulation.API.BL;

public record LoginDTO
{
    public string CardNumber { get; init; }
    public string PIN { get; init; }
}
