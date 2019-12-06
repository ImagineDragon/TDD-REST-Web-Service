using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDD_REST_Web_Service.Models;

namespace TDD_REST_Web_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly DefaultContext _context;

        public DefaultController(DefaultContext context)
        {
            _context = context;
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> GenerateData()
        {
            var random = new Random();
            var defaultModel = new DefaultModel()
            {
                Id = Guid.NewGuid(),
                Field1 = "default",
                Field2 = random.Next(),
                Field3 = "default2",
                Field4 = random.Next()
            };
            _context.DefaultModels.Add(defaultModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataByIdAsync", new { id = defaultModel.Id }, defaultModel);
        }

        // GET: api/Default
        [HttpGet]
        public IEnumerable<DefaultModel> GetAllData()
        {
            return _context.DefaultModels;
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataByIdAsync([FromRoute] Guid id)
        {
            var defaultModel = await _context.DefaultModels.FirstOrDefaultAsync(def => def.Id == id);

            if (defaultModel == null)
            {
                return NotFound();
            }

            return Ok(defaultModel);
        }

        // PUT: api/Default/5
        [HttpPut]
        public async Task<IActionResult> UpdateDataByIdAsync([FromBody] DefaultModel defaultModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.DefaultModels.Any(e => e.Id == defaultModel.Id))
            {
                return NotFound();
            }

            _context.DefaultModels.Update(defaultModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Default/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataById([FromRoute] Guid id)
        {
            var defaultModel = await _context.DefaultModels.FirstOrDefaultAsync(model => model.Id == id);

            if (defaultModel == null)
            {
                return NotFound();
            }

            _context.DefaultModels.Remove(defaultModel);
            await _context.SaveChangesAsync();

            return Ok(defaultModel);
        }
    }
}