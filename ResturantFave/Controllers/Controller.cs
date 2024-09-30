using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantFave.Models;

namespace ResturantFave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        FavRestDbContext dbContext = new FavRestDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? resturant = null)
        {
            List<Post> result = dbContext.Posts.ToList();
            if (resturant != null)
            {
                result = result.Where(p => p.Resturant == resturant).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Post result = dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return NotFound("no matching id");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost()]
        public IActionResult AddPost([FromBody] Post newPost)
        {
            newPost.Id = 0;
            dbContext.Posts.Add(newPost);
            dbContext.SaveChanges();
            return Created($"/api/Posts/{newPost.Id}", newPost);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] Post updated)
        {
            if (updated.Id != id) { return BadRequest("Ids don't match"); }
            if (dbContext.Posts.Any(p => p.Id == id) == false) { return NotFound("No matching ids"); }
            dbContext.Posts.Update(updated);
            dbContext.SaveChanges();
            return Ok(updated);
        }

 
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            Post result = dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return NotFound("No matching id");
            }
            else
            {
                dbContext.Posts.Remove(result);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
