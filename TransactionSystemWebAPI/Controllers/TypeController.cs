using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TransactionSystemWebAPI.Controllers
{
    [Route("api/types")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _typeService.GetAll());
        }
    }
}
