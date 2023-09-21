using ATMSimulation.API.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace ATMSimulation.API;

public class RegisterValidationAttribute: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        RegisterDTO user = context.ActionArguments["registerDTO"] as RegisterDTO;

        #region CardNumber
        string cardNumberPattern = @"^[0-9]{14}$";
        if (!Regex.IsMatch(user.CardNumber, cardNumberPattern))
        {
            context.Result = new BadRequestObjectResult("CardNumber must be a 14-digit positive integer.");
            return;
        }
        #endregion

        #region Name
        string namePattern = @"^[a-zA-Z]{0,100}$";
        if (!Regex.IsMatch(user.Name, namePattern))
        {
            context.Result = new BadRequestObjectResult("Only English letters are allowed, max 100 letter");
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

        #region Balance
        if (user.Balance<0 || user.Balance>999999999)
        {
            context.Result = new BadRequestObjectResult("Invalid Balance Limit.");
            return;
        }
        #endregion

    }
}
