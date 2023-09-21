namespace ATMSimulation.API.BL;

public record RegisterDTO
{
    public string CardNumber { get; init; }
    public string Name { get; init; }
    public string PIN { get; init; }
    public int Balance { get; init; }
}
