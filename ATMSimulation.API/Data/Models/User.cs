using System.ComponentModel.DataAnnotations;

namespace ATMSimulation.API;

public class User
{
    [Key]
    [StringLength(14)]
    public string CardNumber { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(6)]
    public string PIN { get; set; }
    public int Balance { get; set; }
}
