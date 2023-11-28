using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int port = 45693;
            string ip = "82.179.140.18";
            UdpClient client = new UdpClient();

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            string sendData = "Hello World";
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendData);

            client.Send(sendBytes, sendBytes.Length, ep);

            Console.WriteLine("Data sent to {0}", ep.Address);
            Console.ReadLine();


            //try
            //{
            //    // Создаем TCP-клиент и подключаемся к серверу по IP-адресу и порту
            //    TcpClient client = new TcpClient("82.179.140.18", 45693); // Используйте IP-адрес и порт сервера

            //    // Получаем поток для отправки и приема данных
            //    NetworkStream stream = client.GetStream();

            //    // Отправляем сообщение серверу
            //    string message = "Привет, это клиент!";
            //    byte[] data = Encoding.UTF8.GetBytes(message);
            //    stream.Write(data, 0, data.Length);

            //    // Получаем ответ от сервера
            //    byte[] responseBuffer = new byte[1024];
            //    int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            //    string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
            //    Console.WriteLine("Получен ответ: " + response);

            //    // Закрываем соединение
            //    stream.Close();
            //    client.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Ошибка: " + ex.Message);
            //}
        }
    }
}
