// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprTest.Controllers;

[Route("[controller]/[action]")]
public class PubSubController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Publish([FromServices] DaprClient daprClient)
    {
        await daprClient.PublishEventAsync("redis", "test", new
        {
            Message = "test"
        });
        return Ok();
    }

    [HttpPost]
    [Topic("redis", "test")]
    public IActionResult Consume(object msg)
    {
        Console.WriteLine("Consume triggered");
        return Ok();
    }
}
