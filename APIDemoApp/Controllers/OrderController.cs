using APIDemoApp.Models;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIDemoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IFoodData _foodData;
    private readonly IOrderData _orderData;

    public OrderController(IFoodData foodData, IOrderData orderData)
    {
        _foodData = foodData;
        _orderData = orderData;
    }

    // This attribute with the :min(1) return 404 if the condition is not matched
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var order = await _orderData.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        FoodModel food = null!;
        var foods = await _foodData.GetAllFood();
        food = foods.FirstOrDefault(f => f.Id == order.FoodId);

        return Ok(new { order, food });
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderModel model)
    {
        var foods = await _foodData.GetAllFood();

        model.Total = foods.First(f => f.Id == model.FoodId).Price * model.Quantity;

        return Ok(await _orderData.CreateOrderAsync(model));
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] OrderUpdateModel model)
    {
        await _orderData.UpdateOrderName(model.Id, model.NewName);
        return Ok(model.Id);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderData.DeleteOrder(id);

        return Ok();
    }

    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        var foods = await _foodData.GetAllFood();
        var orders = await _orderData.GetAllOrders();

        return Ok(new { foods, orders });
    }

}