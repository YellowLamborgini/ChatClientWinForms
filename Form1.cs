using System.Net.Sockets;

namespace ChatClientWinForms
{
    public partial class Form1 : Form
    {
        TcpClient client;
        string? userName;
        StreamReader? Reader = null;
        StreamWriter? Writer = null;
        public Form1(TcpClient client, string? userName, StreamReader Reader, StreamWriter Writer)
        {
            this.client = client;
            this.userName = userName;
            this.Reader = Reader;
            this.Writer = Writer;
            InitializeComponent();
            // запускаем новый поток для получения данных
            Task.Run(() => ReceiveMessageAsync(Reader));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // сначала отправляем 
            string? message = textBox1.Text;
            Writer.WriteLine(message);
            Writer.Flush();

        }
        async Task ReceiveMessageAsync(StreamReader reader)
        {
            while (true)
            {
                try
                {
                    // считываем ответ в виде строки
                    string? message = await reader.ReadLineAsync();
                    // если пустой ответ, ничего не выводим на консоль
                    if (string.IsNullOrEmpty(message)) continue;
                    listBox1.Items.Add(message);
                }
                catch
                {
                    break;
                }
            }
        }
    }
}
