using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public Response Register(AccountDTO accountDTO)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                // Nếu username hoặc email đã tồn tại
                if(_accountService.CheckUserExist(accountDTO, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                // đăng kí: hash pass, tạo user mới ...
                _accountService.Register(accountDTO);
            } catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            response.Msg = "Register sucess";
            return response;
        }
        

        [HttpPost("login")]
        /// <summary>
        /// new côment 
        /// </summary>
        /// <param name="account">tài khỏ</param>
        /// <returns></returns>
        public Response Login(AccountLogin account)
        {
            decimal x;
            decimal.TryParse("", out x);

            Response response = new Response();
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }
            try
            {
                // Nếu thông tin đăng nhập ko đúng
                if (!_accountService.VerifyLoginInfo(account.LoginName, account.Password, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                //string token = _accountService.CreateToken(account.LoginName);
                AccountDTO accountDTO = _accountService.CreateToken(account.LoginName);
                //_accountService.GenAndSetRefreshToken(Response, account.LoginName);

                //response.ReturnObj = token;
                response.ReturnObj = accountDTO;
                response.Msg = "Login sucess";
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            
            return response;
        }


        [HttpPost("refresh-token"), Authorize]
        public Response RefreshToken([FromBody] string refreshToken)
        {
            Response response = new Response();
            //var refreshToken = Request.Cookies["refreshToken"];
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }
            try
            {
                if (!_accountService.CheckValidRefreshToken(refreshToken, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                string curentUsername = _accountService.GetCurrentUsername();

                AccountDTO accountDTO = _accountService.CreateToken(curentUsername);
                //string token = _accountService.CreateToken(curentUsername);
                //_accountService.GenAndSetRefreshToken(Response);

                //response.ReturnObj = token;
                response.ReturnObj = accountDTO;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            response.Msg = "Refresh token sucess";
            return response;
        }


        // Tạm để fix lỗi mất pass trong db
        [HttpPost("change-password")]
        public Response ChangePassword(AccountLogin account)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                // Nếu username hoặc email đã tồn tại
                //if (!_accountService.CheckUserExist(account, out string message))
                //{
                //    response.SetError("sai thông tin");
                //    return response;
                //}

                // Nếu thông tin đăng nhập ko đúng
                //if (!_accountService.VerifyLoginInfo(account.LoginName, account.Password, out string message))
                //{
                //    response.SetError(message);
                //    return response;
                //}

                // đăng kí: hash pass, tạo user mới ...
                _accountService.ChangePassword(account);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            response.Msg = "Changepass sucess";
            return response;
        }
    }
}
