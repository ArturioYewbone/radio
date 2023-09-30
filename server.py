import ffmpeg
from twisted.internet import reactor
from twisted.internet.protocol import DatagramProtocol
from twisted.internet import task


class RTPAudioServer(DatagramProtocol):
    
    def __init__(self):
        self.audio_stream = (
            ffmpeg.input("c:\\Users\\I\\Desktop\\study\\net\\radio\\test.mp3")  # Замените на ваш источник аудио
            .output("rtp://127.0.0.1:1234", format="rtp")
            .run_async(pipe_stdin=True)
        )
        self.clients = set()  # Сюда будем добавлять подключенных клиентов

    #def datagramReceived(self, data, addr):
        # Здесь вы можете обработать полученные RTP-пакеты
        # Например, отправить их другим клиентам или обработать аудио
    
    def connectionMade(self):
        # Метод вызывается, когда клиент подключается
        self.handle_client_connection()
        self.clients.add(self)
    
    def connectionLost(self, reason):
        # Метод вызывается, когда клиент отключается
        self.clients.remove(self)

    def send_audio_to_clients(self, audio_packet):
        for client in self.clients:
            client.transport.write(audio_packet)
    
    def handle_client_connection(self):
        # Ваш код для обработки подключения клиента
        print("Client connected")

if __name__ == "__main__":
    reactor.listenTCP(1234, RTPAudioServer())
    reactor.run()
