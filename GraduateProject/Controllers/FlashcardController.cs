using GP.Business.IService;
using GP.Business.Service;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardService flashcardService;

        public FlashcardController(IFlashcardService flashcardService)
        {
            this.flashcardService = flashcardService;
        }

        [HttpPost("get-flashcard-by-creditid"), Authorize]
        public Response GetFlashcardByCreditId( string username, string creditId )
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
                response.ReturnObj = flashcardService.GetFlashcardByCreditId(creditId, username);
                response.Msg = "Success";
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
    }
}
