using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConnectApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace ConnectApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        public string IpAddres = null;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public String sIp="", sDateTime = "";

        public async Task<IActionResult> OnPostAsync() //Post form kullanýldýðý zaman OnPostAsync kullanýlýr. When you used Post Form, you use OnPostAsync.
        {

            #region Take own ip address and date-time information as dynamic ~ Kendi ip adresimizi ve gün-zaman bilgilerimizi dinamik olarak almak.
            IPAddress[] addr = (Dns.GetHostEntry(Dns.GetHostName())).AddressList;
            
            sIp = addr[addr.Length - 1].ToString();
            
            sDateTime = DateTime.Now.ToString();

            #endregion

            #region Connect Python for SQLite

            ConnectPython();
            
            #endregion



            #region Ubuntu


            NodeJsConnect();




            #endregion


            #region SQLServer Connected
            //Firstly, you have to do change your settings in the SQL Server Configuration Manager. ~ Öncelikle SQL Server Configuration Manager daki ayarlarýnýzý deðiþtirmek zorundasýnýz.
            ConnectSQLServer();
            
            #endregion


            if (!ModelState.IsValid)
            {
                return Page();//Hatalarý döndürmek için. ~ For send to back errors.
            }
            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
        #region SQL Server
        private void ConnectSQLServer()
        {
            String connectString;
            SqlConnection cnn;
            String sql = "";
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connectString = @"Data Source=192.168.211.135; Initial Catalog=Connect_SEDA;User ID=sa; Password=passwd; Timeout=45";
            //@"Data Source=Your want connection's ip address; Initial Catalog=Your Database's Name; User ID=Your login's Name; Password=Your password"
            //@"Data Source=Baðlanmak istediðiniz ip adresi; Initial Catalog=Veritabanýnýzýn adý; User ID=Giriþ adýnýz; Password=Þifreniz"
            cnn = new SqlConnection(connectString);
            try
            {
                cnn.Open();
                sql = "insert into SEDA_Computer (Name,SurName,Information) values ('" + Customer.Name + "', ' " + Customer.SurName + "', '" + sIp + " " + sDateTime + "')"; 
                command = new SqlCommand(sql, cnn);
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
            }
            catch
            {
                Debug.WriteLine("Baðlanýlmadý");
                cnn.Close();
            }

            Debug.WriteLine("Baðlanýldý.");
        }
        #endregion

        #region Python for SQLite
        private void ConnectPython() // C# has client code, Python has server code. ~ C# ta client kodu, Python da sever kodu var.
        {
            try
            {
                TcpClient tcpClient = new TcpClient();

                tcpClient.Connect("192.168.211.139", 7000);//Your want connection ip address and socket number ~ baðlanmak istediðiniz ip adresi ve soket numarasý
                String sMessage = Customer.Name + " " + Customer.SurName + " " + sIp + " " + sDateTime;
                ASCIIEncoding aSCIImessage = new ASCIIEncoding();

                NetworkStream stm = tcpClient.GetStream();

                byte[] bAsciiMessage = aSCIImessage.GetBytes(sMessage);

                stm.Write(bAsciiMessage, 0, bAsciiMessage.Length);

                tcpClient.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:  " + e.StackTrace);
            }
        }
        #endregion

        #region Ubuntu ~ Node.js
        private void NodeJsConnect()// C# has client code, Node.js has server code. ~ C# ta client kodu, Python da sever kodu var.
        {

            try
            {
                TcpClient tcpClient = new TcpClient();

                tcpClient.Connect("192.168.211.130", 8000); //Your want connection ip address and socket number ~ baðlanmak istediðiniz ip adresi ve soket numarasý
                String sMessage = Customer.Name + " " + Customer.SurName + " " + sIp + " " + sDateTime;
                ASCIIEncoding aSCIImessage = new ASCIIEncoding();

                NetworkStream stm = tcpClient.GetStream();

                byte[] bAsciiMessage = aSCIImessage.GetBytes(sMessage);

                stm.Write(bAsciiMessage, 0, bAsciiMessage.Length);

                tcpClient.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error:  " + e.StackTrace);
            }

        }
        #endregion

    }
}