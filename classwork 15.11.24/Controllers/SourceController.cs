using AutoMapper;
using classwork.Core.Interfaces;
using classwork.Core.Models;
using classwork_15._11._24.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace classwork_15._11._24.Controllers
{
    [Authorize]
    [ApiController]
    [Route("sources")]
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        private readonly IMapper _mapper;

        public SourceController(ISourceService sourceService, IMapper mapper)
        {
            _sourceService = sourceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Source>>> GetSources([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var sources = await _sourceService.GetSources(skip, take);

            return Ok(sources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Source>> GetSourceById([FromRoute] int id)
        {
            var source = await _sourceService.GetSourceById(id);

            if (source is null)
            {
                return NotFound();
            }

            return Ok(source);
        }

        [HttpPost]
        public async Task<ActionResult<Source>> AddSource([FromBody] SourceDTO source)
        {
            var createdSource = await _sourceService.AddSource(_mapper.Map<Source>(source));

            return Created($"sources/{createdSource.Id}", createdSource);
        }
    }
}
