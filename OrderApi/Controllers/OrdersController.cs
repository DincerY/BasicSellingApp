using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderApi.Repository;
using OrderApi.Repository.Entities;
using RabbitMQ.Client;

namespace OrderApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly IModel _channel;
    public OrdersController(OrderDbContext context)
    {
        _context = context;
        ConnectionFactory factory = new();
        IConnection connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> OrderSucceeded(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);

        var result = JsonConvert.SerializeObject(order);
        var resultByte = Encoding.UTF8.GetBytes(result);

        _channel.ExchangeDeclare("success_direct", type: ExchangeType.Direct);

        _channel.BasicPublish(
            exchange: "success_direct",
            routingKey: "success",
            basicProperties: null,
            body : resultByte
            );

        return Ok();
    }

    [HttpGet("fail/{id}")]
    public async Task<IActionResult> OrderFailed(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);

        var result = JsonConvert.SerializeObject(order);
        var resultByte = Encoding.UTF8.GetBytes(result);

        _channel.ExchangeDeclare("success_direct", type: ExchangeType.Direct);

        _channel.BasicPublish(
            exchange: "success_direct",
            routingKey: "fail",
            body: resultByte
        );

        return Ok();
    }

}

