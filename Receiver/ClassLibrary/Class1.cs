using System;
using System.Net;
using System.Net.Sockets;
using NAudio.Wave;
using System.Threading;

namespace ClassLibrary
{
    public class AudioClient
    {
        private static string ip = "82.179.140.18";
        private static int port = 45684;
        private static bool stopReceiving = false;
        static public void Start()
        {
            try
            {
                // Создаем объект для декодирования аудио
                var waveFormat = WaveFormat.CreateMuLawFormat(8000, 1); // Пример для кодека μ-law
                var waveProvider = new BufferedWaveProvider(waveFormat);
                // Создаем соксет
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // IP-адрес и порт сервера, к которому вы хотите подключиться
                IPAddress serverIPAddress = IPAddress.Parse(ip);
                int serverPort = port;

                // Создаем объект EndPoint для сервера
                IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, serverPort);

                // Подключаемся к серверу
                clientSocket.Connect(serverEndPoint);

                Console.WriteLine("Подключение к серверу успешно!");
                byte[] buffer = new byte[1024];
                // Создаем поток для приема данных
                Thread receivingThread = new Thread(() =>
                {
                    while (!stopReceiving)
                    {
                        // Принимаем RTP-пакет
                        //int bytesRead = clientSocket.Receive(buffer);

                        // Декодируем аудио из RTP-пакета и воспроизводим
                       // waveProvider.AddSamples(buffer, 0, bytesRead);

                        // Воспроизводим аудио (например, через воспроизведение NAudio)
                        //var waveOut = new WaveOut();
                        //waveOut.Init(waveProvider);
                        //waveOut.Play();
                        //Console.WriteLine($"Принято {bytesRead} байт данных.");
                    }
                });
                receivingThread.Start();
                // Ожидание нажатия пробела для остановки приема
                Console.WriteLine("Нажмите пробел для остановки приема.");
                while (Console.ReadKey().Key != ConsoleKey.Spacebar) {
                    // Устанавливаем флаг остановки приема
                    stopReceiving = true;
                    // Ожидаем завершения потока приема
                    receivingThread.Join();
                    // Закрываем соксет после использования
                    clientSocket.Close();
                }
                

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}



