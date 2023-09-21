using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATMSimulation.API.BL;

public class UserServices : IUserServices
{
    private readonly UserContext _context;
    private readonly IConfiguration _configuration;
    public UserServices(UserContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    #region Register
    public async Task<string> RegisterAsync(RegisterDTO registerDTO)
    {
        //Check existing same card number 
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.CardNumber == registerDTO.CardNumber);
        if (existingUser != null)
        {
            return "User with same CardNumber already exists";
        }

        //Create new User 
        var newUser = new User
        {
            CardNumber = registerDTO.CardNumber,
            Name = registerDTO.Name,
            PIN = registerDTO.PIN,
            Balance = registerDTO.Balance
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return "User registered successfully";
    }
    #endregion

    #region Login
    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        //get user by card number
        var user = await _context.Users.FirstOrDefaultAsync(u => u.CardNumber == loginDTO.CardNumber);

        if (user == null)
        {
            return "Incorrect Card Number Not Found.";
        }

        if (user.PIN != loginDTO.PIN)
        {
            return "Incorrect PIN.";
        }
        return null;
    }
    #endregion

    #region Check Balance
    public async Task<int> CheckBalanceAsync(string cardNumber)
    {
        //get user by card number
        var user = await _context.Users.FirstOrDefaultAsync(u => u.CardNumber == cardNumber);

        if (user != null)
        {
            return user.Balance;
        }

        return -1; //User not found (Auth)
    }
    #endregion

    #region Cash Withdrawal
    public async Task<string> CashWithdrawalAsync(string cardNumber, int cash)
    {
        //get user by card number
        var user = await _context.Users.FirstOrDefaultAsync(u => u.CardNumber == cardNumber);

        if (user != null)
        {
            //check balance && cash limit
            if (user.Balance >= cash && cash <= 1000)
            {
                user.Balance -= cash;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return "Cash withdrawal successfully.";
            }
            else
            {
                return "balance low OR withdrawal limit 1000 exceeded.";
            }
        }

        return "User not found.";
    }
    #endregion

    #region Generate Token
    public string GenerateJwtToken(string cardNumber)
    {
        var claim = new Claim(ClaimTypes.Name, cardNumber);
        var _claims = new List<Claim> { claim };

        //Generate Key
        var secretKey = _configuration.GetValue<string>("SecretKey");
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
        var key = new SymmetricSecurityKey(secretKeyInBytes);

        //Hashing
        var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var exp = DateTime.Now.AddMinutes(30);

        //Genete Token 
        var jwt = new JwtSecurityToken(
            claims: _claims,
            notBefore: DateTime.Now,
            expires: exp,
            signingCredentials: methodUsedInGeneratingToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        string tokenStr = tokenHandler.WriteToken(jwt);

        return tokenStr;
    }
    #endregion

}
