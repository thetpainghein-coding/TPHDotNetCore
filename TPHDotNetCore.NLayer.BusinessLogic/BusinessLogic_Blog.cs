using Microsoft.EntityFrameworkCore;
using TPHDotNetCore.NLayer.DataAccess.Services;
using TPHDotNetCore.NLayer.DataAccess.Models;

namespace TPHDotNetCore.NLayer.BusinessLogic
{
    public class BusinessLogic_Blog
    {
        private readonly DataAccess_Blog _da_Blog;

        public BusinessLogic_Blog()
        {
            _da_Blog = new DataAccess_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _da_Blog.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _da_Blog.GetBlog(id);
            return item;
        }

        public int CreateBlog(string v, string v1, BlogModel requestModel)
        {

            var result = _da_Blog.CreateBlog(requestModel);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {

            var result = _da_Blog.UpdateBlog(id, requestModel);
            return result;
        }

        public int DeleteBlog(int id)
        {

            var result = _da_Blog.DeleteBlog(id);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var result = _da_Blog.PatchBlog(id, requestModel);
            return result;
        }

        public void CreateBlog(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}
