using ATMSimulation.API.BL;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ATMSimulation.API;

public class LoginValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        LoginDTO user = context.ActionArguments["credentials"] as LoginDTO;

        #region CardNumber
        string cardNumberPattern = @"^[0-9]{14}$";
        if (!Regex.IsMatch(user.CardNumber, cardNumberPattern))
        {
            context.Result = new BadRequestObjectResult("CardNumber must be a 14-digit positive integer.");
            return;
        }
        #endregion

        #region PIN
        string PINPattern = @"^[0-9]{6}$";
        if (!Regex.IsMatch(user.PIN, PINPattern))
        {
            context.Result = new BadRequestObjectResult("PIN must be a 6-digit positive integer.");
            return;
        }
        #endregion
    }
}
