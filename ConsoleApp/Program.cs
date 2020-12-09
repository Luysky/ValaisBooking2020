 using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Http.Headers;
using BLL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();



        static void Main(string[] args)
        {

        }
    }


}
