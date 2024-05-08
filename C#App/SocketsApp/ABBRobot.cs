using System.Net.Sockets;
using System.Text;

namespace ABBSockets
{
    internal class ABBRobot
    {
        private TcpClient TcpClient;
        private NetworkStream ClientStream;

        public bool isSocketOpened 
        { 
            get 
            { 
                return ClientStream != null & TcpClient != null; 
            } 
        }


        public ABBRobot()
        {
        }

        public void OpenSocket(string IP = "127.0.0.1", int PORT = 4000)
        {
            TcpClient = new TcpClient(IP, PORT);
            ClientStream = TcpClient.GetStream();
        }
        public void CloseSocket()
        {
            if (TcpClient is null) return;

            TcpClient.Close();
        }

        public void SendDataToRobot(string data)
        {
            if (ClientStream is null) return;

            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);
            ClientStream.Write(bytesToSend, 0, bytesToSend.Length);
        } // to Robot
        public string ReceiveDataFromRobot()
        {
            if (ClientStream is null) return "";

            byte[] bytesToRead = new byte[TcpClient.ReceiveBufferSize];
            int bytesRead = ClientStream.Read(bytesToRead, 0, TcpClient.ReceiveBufferSize);
            string receivedMessage = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

            return receivedMessage;

        } // from Robot
    }
}
