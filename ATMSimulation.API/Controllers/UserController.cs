using ATMSimulation.API.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMSimulation.API;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _user;
    public UserController(IUserServices user)
    {
        _user = user;
    }

    [HttpPost]
    [Route("login")]
    [LoginValidation]
    public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
    {
        var message = await _user.LoginAsync(credentials);

        if (message == null)
        {
            var token = _user.GenerateJwtToken(credentials.CardNumber);
            var exp = DateTime.Now.AddMinutes(30);
            return Ok(new TokenDTO { Token = token , ExpiryDate=exp });
        }

        return Unauthorized(message); //Auth failed
    }

    [HttpPost]
    [Route("register")]
    [RegisterValidation]
    public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
    {
        var result = await _user.RegisterAsync(registerDTO);

        if (result == "User registered successfully.")
        {
            return Ok(result);
        }
        return BadRequest(result); 
    }

    [Authorize]
    [HttpGet]
    [Route("balance")]
    public async Task<ActionResult<int>> CheckBalance()
    {
        // Get cardNu by auth user claims
        var cardNumber = User.Identity.Name; 

        var balance = await _user.CheckBalanceAsync(cardNumber);
        return Ok(balance);
    }
    
    [Authorize]
    [HttpPost]
    [Route("Cashwithdrawal")]
    public async Task<ActionResult<string>> CashWithdrawal(int cash)
    {
        var cardNumber = User.Identity.Name; 

        var result = await _user.CashWithdrawalAsync(cardNumber, cash);

        if (result == "Cash withdrawal successful.")
        {
            return Ok(result);
        }
        return BadRequest(result); 
    }
}
