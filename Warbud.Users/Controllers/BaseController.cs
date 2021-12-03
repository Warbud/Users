﻿using Microsoft.AspNetCore.Mvc;

namespace Warbud.Users.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController: ControllerBase
    {

        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is null ? NotFound() : Ok(result);    }
}