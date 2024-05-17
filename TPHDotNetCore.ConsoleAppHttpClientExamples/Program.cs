// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TPHDotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello, World!");

//Console App => Client side (Frontend)
//Asp.Net Core Web Api => Server (Backend)

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();