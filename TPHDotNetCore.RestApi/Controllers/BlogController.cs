using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPHDotNetCore.RestApi.Db;
using TPHDotNetCore.RestApi.Models;

namespace TPHDotNetCore.RestApi.Controllers
{
    // https://localhost:3000 => domain
    // api/blog => end point
    [Route("api/[controller]")] //[controller] => template
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        //public BlogController()
        //{
        //    _context = new AppDbContext();
        //}

       public BlogController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst); // http status 200
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item); // http status 200
        }


        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message); // http status 200
        }


        [HttpPut]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = _context.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";


            return Ok(message); // http status 200
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor= blog.BlogAuthor;
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent= blog.BlogContent;
            }

            var result = _context.SaveChanges();
            string message = result > 0 ? "Patching Successful" : "Patching Failed";


            return Ok(message); // http status 200

           
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

             _context.Blogs.Remove(item);


            var result = _context.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message); // http status 200
        }


    }
}
