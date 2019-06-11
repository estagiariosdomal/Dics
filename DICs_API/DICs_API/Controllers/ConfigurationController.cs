﻿using DICs_API.Errors;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace DICs_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationRepository _repoConfiguration;
        public ConfigurationController(IConfiguration configuration){
            _repoConfiguration = new ConfigurationRepository(configuration);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Recupera configuração identificada pelo seu {id}.",
                          Tags = new[] { "Configuration" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(Configuration))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoConfiguration.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera todas as configurações.",
                          Tags = new[] { "Configuration" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(Configuration))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult GetAll()
        {
            var list = _repoConfiguration.GetAll();
            return Ok(list);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Altera uma configuração.",
                          Tags = new[] { "Configuration" })]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Update ([Bind("Id, Period")]ConfigurationUpload configuration)
        {
            if (ModelState.IsValid)
            {
                _repoConfiguration.Update(configuration);
                return Ok();
            }
            return BadRequest();
        }
   
    }
}
