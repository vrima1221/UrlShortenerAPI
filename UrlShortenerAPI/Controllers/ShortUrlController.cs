using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testAPI.Models;
using testAPI.Data;

namespace testAPI.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly ApiContext _context;

        public ShortUrlController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(ShortURLs shortURL)
        {
            if(shortURL.id == 0) 
            {
                _context.ShortURLs.Add(shortURL);
            }
            else
            {
                var shortUrlInDb = _context.ShortURLs.Find(shortURL.id);

                if(shortUrlInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                shortUrlInDb = shortURL;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(shortURL));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.ShortURLs.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.ShortURLs.Find(id);

            if(result == null)
            { 
                return new JsonResult(NotFound()); 
            }

            _context.ShortURLs.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.ShortURLs.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public IActionResult GetShort(string shortUrl)
        {
            var result = _context.ShortURLs.FirstOrDefault(s => s.shortUrl == shortUrl);

            if (result == null)
            {
                return NotFound();
            }

            return Redirect(result.fullUrl);
        }
    }
}
