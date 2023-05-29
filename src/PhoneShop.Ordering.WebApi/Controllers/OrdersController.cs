using Microsoft.AspNetCore.Mvc;
using PhoneShop.Ordering.Application.Orders.Commands.CheckoutOrder.v1;
using PhoneShop.Ordering.Application.Orders.Commands.DeleteOrder.v1;
using PhoneShop.Ordering.Application.Orders.Commands.UpdateOrder.v1;
using PhoneShop.Ordering.Application.Orders.Queries.GetOrderList.v1;
using PhoneShop.Utilities.Models;

namespace PhoneShop.Ordering.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ApiControllerBase
{
    public OrdersController()
    {
        
    }

    [HttpGet(Name = "GetOrders")]
    public async Task<IActionResult> GetOrdersByUsername(GetOrderListQuery request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }
    }

    [HttpPost(Name = "CheckoutOrder")]
    public async Task<IActionResult> CheckoutOrder(CheckoutOrderCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }
    }

    [HttpPost(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(UpdateOrderCommand request)
    {
        try
        {
            await Mediator.Send(request);
            return Ok(BaseResponse.Ok(true));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }
    }

    [HttpDelete(Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(DeleteOrderCommand request)
    {
        try
        {
            await Mediator.Send(request);
            return Ok(BaseResponse.Ok(true));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }
    }

}
