using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TPHDotNetCore.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TPHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            Console.WriteLine("Connection Open");

            string query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach(DataRow dr in dt.Rows)
            //{
            //    BlogModel blog = new BlogModel();
            //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]); 
            //    lst.Add(blog);  
            //}


            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();


            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            //Console.WriteLine("Connection Open");

           
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);

        }



        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog) 
        {
            

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
			@BlogAuthor,
			@BlogContent)"
            ;

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();


            connection.Close();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);

            //return StatusCode(500, message);


        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM Tbl_Blog
      WHERE BlogId = @BlogId"
            ;


            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);


            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();


            connection.Close();

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
            WHERE BlogId = @BlogId"
            ;

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            blog.BlogId = id;
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd.ExecuteNonQuery();


            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }

            if (condition.Length == 0)
            {
                return NotFound("No data to Update");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";

            blog.BlogId = id;
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Successful" : "Updating Failed";


            return Ok(message);
        }



        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
