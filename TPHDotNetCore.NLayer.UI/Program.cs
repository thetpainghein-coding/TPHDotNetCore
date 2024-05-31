// See https://aka.ms/new-console-template for more information
using TPHDotNetCore.NLayer.BusinessLogic;

Console.WriteLine("Hello, World!");

BusinessLogic_Blog bl_Blog  = new BusinessLogic_Blog();

var item = bl_Blog.GetBlog(7);
Console.WriteLine(item.ToString());
