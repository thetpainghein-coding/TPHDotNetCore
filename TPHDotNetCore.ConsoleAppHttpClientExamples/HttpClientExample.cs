using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace TPHDotNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly HttpClient  _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5115") }; // Uri => base domain Url
        private readonly string _blogEndpoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(15);

            //await CreateAsync("title", "author 2", "content 3");

            await EditAsync(15);

            await PatchAsync(15, "", "Author sekfmdok","");

            await EditAsync(15);
        }

        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndpoint); // task == one job assigned(has not done it without await)

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync(); // Content => body value                                                                         //Console.WriteLine(jsonStr);

                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"Title => {blog.BlogTitle}");
                    Console.WriteLine($"Author => {blog.BlogAuthor}");
                    Console.WriteLine($"Content => {blog.BlogContent}");

                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndpoint}/{id}"); // task == one job assigned(has not done it without await)

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync(); // Content => body value                                                                         //Console.WriteLine(jsonStr);

                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
                
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");             
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndpoint}/{id}"); // task == one job assigned(has not done it without await)

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                // for other process
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string author,string content)
        {
            BlogModel blogmodel = new BlogModel() // C# object
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // c# to json
            string blogJson = JsonConvert.SerializeObject(blogmodel);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json );
            var response = await _client.PostAsync(_blogEndpoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string? title, string? author, string? content)
        {
            BlogModel blogmodel = new BlogModel() // C# object
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // c# to json
            string blogJson = JsonConvert.SerializeObject(blogmodel);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task PatchAsync(int id, string title, string author, string content)
        {
            BlogModel blogmodel = new BlogModel() // C# object
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            // c# to json
            string blogJson = JsonConvert.SerializeObject(blogmodel);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
