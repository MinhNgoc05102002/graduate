using GP.Business.IService;
using GP.Business.Service;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("get-noti"), Authorize]
        public Response GetHeaderNotification(SearchBase searchBase)
        {
            Response response = new Response();
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status400BadRequest, "Validate Error");
                return response;
            }
            try
            {
                response.ReturnObj = _accountService.GetNotiByUser(searchBase);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex;
            }
            response.Msg = "Sucess";
            return response;
        }

        [HttpPost("get-list-account-by-filter"), Authorize]
        public Response GetListAccountByFilter(SearchBase searchBase)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status400BadRequest, "Validate Error");
                return response;
            }
            try
            {
                response.ReturnObj = _accountService.GetCreditByFilter(searchBase);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex;
            }
            response.Msg = "Success";
            return response;
        }

        [HttpPost("get-account-by-username"), Authorize]
        public Response GetAccountByUsername([FromBody] string username)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status400BadRequest, "Validate Error");
                return response;
            }
            try
            {
                response.ReturnObj = _accountService.GetAccountByUsername(username);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex;
            }
            response.Msg = "Success";
            return response;
        }
    }
}
