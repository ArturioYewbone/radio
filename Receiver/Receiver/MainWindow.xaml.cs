using System;
using System.Net;
using System.Net.Sockets;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClassLibrary;

namespace Receiver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;
        private SoundPlayer soundPlayer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Установите IP-адрес и порт сервера RTP
                serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

                // Создайте клиентское UDP-соединение
                udpClient = new UdpClient();
                udpClient.Connect(serverEndPoint);

                // Создайте SoundPlayer для воспроизведения аудио
                soundPlayer = new SoundPlayer();

                // Начните принимать и воспроизводить аудио
                byte[] audioData;
                while (true)
                {
                    audioData = udpClient.Receive(ref serverEndPoint);
                    PlayAudio(audioData);
                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        // Выходим из цикла, если нажата клавиша пробела
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private void PlayAudio(byte[] audioData)
        {
            try
            {
                // Создайте временный WAV-файл из аудио-данных
                string tempFileName = Guid.NewGuid().ToString() + ".wav";
                System.IO.File.WriteAllBytes(tempFileName, audioData);

                // Загрузите и воспроизведите временный WAV-файл
                soundPlayer.SoundLocation = tempFileName;
                soundPlayer.Load();
                soundPlayer.Play();

                // Удалите временный WAV-файл
                System.IO.File.Delete(tempFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения аудио: " + ex.Message);
            }
        }
    }
}
