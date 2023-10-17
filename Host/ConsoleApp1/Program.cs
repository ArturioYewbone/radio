using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем сокет
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Устанавливаем адрес и порт сервера, к которому хотим подключиться
            string serverAddress = "82.179.140.18"; // Замените на IP-адрес или домен вашего сервера
            int serverPort = 45127
                сд; // Замените на порт вашего сервера

            // Создаем объект для хранения информации об удаленном сервере
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);

            try
            {
                // Подключаемся к серверу
                clientSocket.Connect(serverAddress, serverPort);
                Console.WriteLine("Успешно подключено к серверу.");

                // В этот момент вы можете отправлять и принимать данные через сокс.
                // Например, используйте clientSocket.Send и clientSocket.Receive.

                // После завершения работы, закройте сокс.
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при подключении к серверу: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
