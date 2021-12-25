// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprTest.Controllers
{
    [Route("[controller]/[action]")]
    public class ServiceInvocationController : ControllerBase
    {
        [HttpGet]
        [HttpPost]
        public IActionResult Hello()
        {
            return Ok(new
            {
                Msg = "Hello world"
            });
        }

        [HttpGet]
        public async Task<IActionResult> Test([FromServices] DaprClient daprClient)
        {
            var result = await daprClient.InvokeMethodAsync<object>("dapr-test", "ServiceInvocation/Hello");
            return Ok(new { Result = result });
        }
    }
}
