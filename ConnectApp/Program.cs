/*
Author = Fatma Seda ÖZYURT
Create Date = 23.10.18
Purpose = This project's purpose is connect the other pc`s and do sharing data between pc`s in the same network
          Projenin amacı diğer bilgisayarlarla bağlantı kurmak ve diğer bilgisayarlar arasında veri paylaşımı yapmak.
Done Operation = A Core Pc is connected the other pc's SQL Server, the other pc's SQLite and the other pc's Node.js of Ubuntu. 
                 It is connected remote with SQL Server and it is connected with socket programming Python for remote connection SQLite and with socket programming Node.js for remote connection Ubuntu. 
                 You can look this section from Create.cshtml.cs
                 
                 Bir ana bilgisayardan diğer bilgisayardaki SQL Server a, SQLite a ve Ubuntu daki Node.js e bağlantı kurulur.
                 SQL Server ile uzaktan bağlantı sağlanır, SQLite a uzaktan bağlantı sağlamak için Python ile soket programlama yapılır ve Ubuntu ya uzaktan bağlantı sağlamak için Node.js ile soket programlama yapılır.
                 Create.cshtml.cs bölümünden bakabilirsiniz.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConnectApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
