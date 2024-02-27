namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models;
    public class ErrorController : BaseController
    {
        [AllowAnonymous]
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var errorMsg = statusCode switch
            {
                404 => "Error not found!",
                401 => "Unauthorized access!",
                405 => "Method Not Allowed",
                500 => "Internal Server error!",
                _ => "Unexpected error occured!"
            };

            var model = new GenericErrorViewModel()
            {
                StatusCode = statusCode,
                Error = errorMsg
            };
            return View(model);
        }
    }
}
