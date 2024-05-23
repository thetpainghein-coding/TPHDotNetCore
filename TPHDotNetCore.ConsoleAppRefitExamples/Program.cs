using System.Reflection.Metadata;
using Refit;
using TPHDotNetCore.ConsoleAppRefitExamples;



try
{
    RefitExamples refitExamples = new RefitExamples();
    await refitExamples.RunAsync();
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}