using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn.Models;
using Microsoft.AspNetCore.Mvc;

namespace learn.API
{
    [Route("api/[controller]")]
    [ApiController]  

    public class MessagesController : Controller
    {
        private readonly CrudExampleContext _context;
        public MessagesController(CrudExampleContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Messages.Where(m => m.IsDeleted.HasValue && !m.IsDeleted.Value).ToList();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var result = _context.Messages.Where(m => m.Id == id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] string value)
        {
            if (!_context.Messages.Any(m => m.Id == id))
            {
                return NotFound();
            }
            var message = _context.Messages.Where(m => m.Id == id).Single();
            message.Message = value;
            _context.Update(message);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromForm] string value)
        {
            var message = new Messages
            {
                Date = DateTime.Now,
                Message = value
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_context.Messages.Any(m =>m.Id == id))
            {
                return NotFound();
            }

            var message = _context.Messages.Where(m => m.Id == id).Single();
            message.IsDeleted = true;
            _context.Update(message);
            _context.SaveChanges();
            return Ok();
        }
    }
}