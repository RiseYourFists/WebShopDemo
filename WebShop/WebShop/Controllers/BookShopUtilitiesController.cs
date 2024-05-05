namespace WebShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using WebShop.Services.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class BookShopUtilitiesController : ControllerBase
    {
        private readonly IBookShopUtilityService _service;
        public BookShopUtilitiesController(IBookShopUtilityService service)
        {
            _service = service;
        }

        [HttpGet("/api/GetBooks/{searchTerm?}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSearchResults(string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return NotFound();
            }

            var result = await _service.GetSearchResults(searchTerm);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(result);

            return Content(json, "application/json");
        }
    }
}
