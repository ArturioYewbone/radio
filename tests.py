import socket

# Создаем серверный соксет
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# IP-адрес и порт, на котором сервер будет прослушивать подключения
server_ip = '127.0.0.1'  # Можете использовать '0.0.0.0', чтобы прослушивать на всех интерфейсах
server_port = 8080

# Связываем серверный соксет с IP-адресом и портом
server_socket.bind((server_ip, server_port))

# Начинаем прослушивать подключения (с максимальным количеством ожидаемых клиентов в очереди)
server_socket.listen(5)

print(f"Сервер слушает на {server_ip}:{server_port}")

while True:
    # Принимаем клиентское подключение
    client_socket, client_address = server_socket.accept()

    print(f"Принято подключение от {client_address}")

    # Здесь вы можете взаимодействовать с клиентом, отправлять и получать данные
    # Например, client_socket.send() и client_socket.recv() для отправки и приема данных

    # Закрытие соединения с клиентом
    client_socket.close()
