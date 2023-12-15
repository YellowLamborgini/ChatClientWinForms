using System.Net.Sockets;
using System.Windows.Forms;

namespace ChatClientWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            string host = "127.0.0.1";
            int port = 8888;
            TcpClient client = new TcpClient();
            string? userName = "TestName";
            StreamReader? Reader = null;
            StreamWriter? Writer = null;
            try
            {
                client.Connect(host, port); //подключение клиента
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
                if (Writer is null || Reader is null) return;
                Writer.WriteLine(userName);
                Writer.Flush();
                
            }
            catch (Exception ex)
            {
            }
            

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(client, userName, Reader, Writer));

            Writer?.Close();
            Reader?.Close();
        }
       
    }
}