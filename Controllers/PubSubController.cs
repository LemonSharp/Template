// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprTest.Controllers;

[Route("[controller]/[action]")]
public class PubSubController : ControllerBase
{
    private readonly ILogger _logger;

    public PubSubController(ILogger<PubSubController> logger)
    {
        _logger = logger;
    }

    private static int num;
    [HttpPost]
    public async Task<IActionResult> Publish([FromServices] DaprClient daprClient)
    {
        await daprClient.PublishEventAsync("pubsub", "test", new
        {
            Message = "test"
        });
        return Ok();
    }

    [HttpPost]
    [Topic("pubsub", "test")]
    public IActionResult Consume(object msg)
    {
        _logger.LogInformation("Consumed message: {msg}", System.Text.Json.JsonSerializer.Serialize(msg));
        num++;
        return Ok(msg);
    }

    [HttpGet]
    public IActionResult Counter()
    {
        return Ok(new
        {
            Num = num
        });
    }
}
