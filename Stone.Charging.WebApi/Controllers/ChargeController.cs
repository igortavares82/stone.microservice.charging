﻿using Microsoft.AspNetCore.Mvc;
using Stone.Charging.Application.Abstractions;
using Stone.Charging.Messages;
using Stone.Framework.Result.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stone.Charging.WebApi.Controllers
{
    [Route("api/charge")]
    public class ChargeController : Controller
    {
        private IChargeApplication ChargeApplication { get; }

        public ChargeController(IChargeApplication chargeApplication)
        {
            ChargeApplication = chargeApplication;
        }

        [HttpGet("wellcome"), Produces("text/plain", Type = typeof(string))]
        public string GetWellcome() => "Wellcome to charging service!";

        [HttpGet, Produces("application/json", Type = typeof(IApplicationResult<List<ChargeMessage>>))]
        public virtual async Task<IActionResult> GetAsync([FromQuery] ChargeSearchMessage message)
        {
            return await ChargeApplication.GetAsync(message);
        }

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public virtual async Task<IActionResult> PostAsync([FromBody] ChargeMessage message)
        {
            return await ChargeApplication.RegisterAsync(message);
        }

        [HttpPost("batch"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public virtual async Task<IActionResult> PostAsync([FromBody] List<ChargeMessage> messages)
        {
            return await ChargeApplication.RegisterAsync(messages);
        }
    }
}