// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TPHDotNetCore.ConsoleApp.AdoDotNetExamples;
using TPHDotNetCore.ConsoleApp.DapperExamples;
using TPHDotNetCore.ConsoleApp.EFCoreExamples;
using TPHDotNetCore.ConsoleApp.Services;
using TPHDotNetCore.RestApi.Db;
using AppDbContext = TPHDotNetCore.ConsoleApp.EFCoreExamples.AppDbContext;


Console.WriteLine("Hello, World!");

//nuget package(sqlclient) for database
//F10 => 
//F11 => into the used class


//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();

//stringBuilder.DataSource = "DESKTOP-SOD2VVR"; //servername
//stringBuilder.InitialCatalog = "TPHDotNetCore"; //databasename

//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sa@123";

//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);


//connection.Open();
//Console.WriteLine("Connection Open");

//string query = "select * from Tbl_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection Close");

////dataset => datatable
////datatable => datarow
////datarow => datacolumn

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog ID =>" + dr["BlogID"]);
//    Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content =>" + dr["BlogContent"]);
//    Console.WriteLine("--------------------------");
//}


//Ado.net Read

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

////adoDotNetExample.Read();

////adoDotNetExample.Create("title", "author", "content");

////adoDotNetExample.Update(7, "test title", "test author", "text content");

////adoDotNetExample.Delete(3);

//adoDotNetExample.edit(3);
//adoDotNetExample.edit(6);


//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

var connectionString = ConnectionStrings.SqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))

    //.AddScoped<AdoDotNetExample>(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped< EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();
Console.ReadKey(); // press any key to exit the program