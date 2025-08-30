using DataLibrary.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APIDemoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController : Controller
{
    private readonly IFoodData _foodData;

    public FoodController(IFoodData foodData)
    {
        _foodData = foodData;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _foodData.GetAllFood());
    }
}