from twisted.internet import reactor, protocol
from twisted.internet.protocol import DatagramProtocol
from pydub import AudioSegment
import struct
import time

class RTPPacketGenerator(DatagramProtocol):
    def __init__(self, destination_ip, destination_port, audio_file):
        self.destination_ip = destination_ip
        self.destination_port = destination_port
        self.sequence_number = 0
        self.timestamp = int(time.time())  # Используем текущее время как начальное значение временной метки
        self.payload_type = 0  # Тип аудио данных (зависит от выбранного аудио-кодека)
        self.clock_rate = 8000  # Частота дискретизации аудио
        self.audio_segment = AudioSegment.from_mp3(audio_file)

    def startProtocol(self):
        self.transport.connect(self.destination_ip, self.destination_port)
        self.schedule_packet()

    def schedule_packet(self):
        audio_data = self.audio_segment.raw_data

        # Создаем RTP пакет с увеличивающимся номером последовательности и временной меткой
        rtp_header = struct.pack("!BBHII", (2 << 6) | self.payload_type, 0, self.sequence_number, self.timestamp, 0)
        rtp_packet = rtp_header + audio_data

        # Увеличиваем номер последовательности и временную метку для следующего пакета
        self.sequence_number = (self.sequence_number + 1) & 0xFFFF
        self.timestamp += int(self.clock_rate * len(audio_data) / len(self.audio_segment))

        # Отправляем RTP пакет
        self.transport.write(rtp_packet)

        # Планируем отправку следующего пакета через 100 мс (пример: 10 пакетов в секунду)
        reactor.callLater(0.1, self.schedule_packet)

if __name__ == '__main__':
    destination_ip = '127.0.0.1'  # IP-адрес назначения
    destination_port = 12345  # Порт назначения (должен быть установлен на стороне клиента)
    audio_file = 'c:\\Users\\I\\Desktop\\study\\net\\radio\\test.mp3'  # Файл MP3 для отправки

    protocol_factory = protocol.Factory()
    protocol_factory.protocol = lambda: RTPPacketGenerator(destination_ip, destination_port, audio_file)

    reactor.listenUDP(0, protocol_factory)
    reactor.run()
