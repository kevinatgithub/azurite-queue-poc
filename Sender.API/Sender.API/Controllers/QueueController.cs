using Microsoft.AspNetCore.Mvc;
using Sender.API.Services;

namespace Sender.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueueController : ControllerBase
{
    private readonly QueueService _queueService;

    public QueueController(QueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageAsync(string message)
    {
        await _queueService.SendMessageAsync(message);

        return Ok();
    }
}
