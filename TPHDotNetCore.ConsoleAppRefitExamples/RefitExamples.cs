using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace TPHDotNetCore.ConsoleAppRefitExamples
{
    public class RefitExamples
    {
        IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7238/");
        public async Task RunAsync() 
        {
            await EditAsync(15);
            await UpdateAsync(15, "New Refit", "New Refit", "New Refit");
            await EditAsync(15);

            //await DeleteAsync(20);
            //await ReadAsync();

            //await editasync(15);
            //await patchasync(15, "", "dsfsdds", "");
            //await editasync(15);


        }

        private async Task ReadAsync()
        {         
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"ID => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("_______________________________");
            }
        }

        private async Task EditAsync(int id)
        {
            try
            {
                var item = await _service.GetBlog(id);

                Console.WriteLine($"ID => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("_______________________________");
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }         

        }

        private async Task CreateAsync(string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel()
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                };

                var message = await _service.CreateBlog(blog);
                Console.WriteLine(message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                };
                var message = await _service.UpdateBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
         
        }
        private async Task DeleteAsync(int id)
        {
            try
            {
                var message = await _service.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task PatchAsync(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                };
                var message = await _service.PatchBlog(id, blog);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
