using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TPHDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BusinessLogic_Blog _bl_Blog;


        public BlogController()
        {
            _bl_Blog = new BusinessLogic_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _bl_Blog.GetBlogs();
            return Ok(lst); // http status 200
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item); // http status 200
        }


        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {

            var result = _bl_Blog.CreateBlog(blog);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message); // http status 200
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _bl_Blog.GetBlog(id); // to check the existence of the blog by id
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            var result = _bl_Blog.UpdateBlog(id, blog);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";


            return Ok(message); // http status 200
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            var result = _bl_Blog.PatchBlog(id, blog);
            string message = result > 0 ? "Patching Successful" : "Patching Failed";


            return Ok(message); // http status 200
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
        
            var result = _bl_Blog.DeleteBlog(id);
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message); // http status 200
        }
    }
}
