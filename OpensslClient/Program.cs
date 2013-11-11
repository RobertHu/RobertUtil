using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Permissions;
using CommonUtil;
using System.Threading;

namespace OpensslClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MySocket socket = new MySocket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            socket.Connect(iep);
            MyNetworkStream stream = new MyNetworkStream(socket);
            SslStream sslStream = new SslStream(stream,false,new RemoteCertificateValidationCallback(ValidateServerCertificate),null );
            string msg = "hellodsffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdf,sdfwewwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwfawwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            byte[] packet = new byte[6 + data.Length];
            byte[] lengthBytes = data.Length.ToCustomerBytes();
            Buffer.BlockCopy(lengthBytes, 0, packet, 2, lengthBytes.Length);
            Buffer.BlockCopy(data, 0, packet, 6, data.Length);
            sslStream.AuthenticateAsClient("127.0.0.1");
            Client client = new Client(sslStream);

            byte[] head = new byte[6];
            int contentLength = 50;
            byte[] contentLengthBytes = contentLength.ToCustomerBytes();
            Array.Copy(contentLengthBytes, 0, head, 2, contentLengthBytes.Length);
            client.Write(head);

            Thread.Sleep(1000);

            int firstPart = 25;
            client.Write(new byte[firstPart]);

            Thread.Sleep(1000);
            client.Write(new byte[contentLength - firstPart]);

            byte[] buff = new byte[1024];
            sslStream.BeginRead(buff, 0, buff.Length, ar =>
                {
                    int len=sslStream.EndRead(ar);
                    Console.WriteLine(len);
                }, null);
            Console.Read();

        }


        private static byte[] BuildCustmerPacket()
        {
            int contentLength = 100;
            int sessionLength = 0;
            int currentSend =1;
            int packetLength = currentSend + sessionLength + 6;
            byte[] packet = new byte[packetLength];
            byte[] contentBytes = contentLength.ToCustomerBytes();
            Array.Copy(contentBytes, 0, packet, 2, contentBytes.Length);
            return packet;
        }


        private static byte[] BuildKeepAlivePacket()
        {
            string content = "my keep alive";
            string session = "55";
            byte[] sessionBytes = Encoding.ASCII.GetBytes(session);
            int sessionLength  = sessionBytes.Length;
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            byte[] contentLengthBytes = contentBytes.Length.ToCustomerBytes();
            byte[] packet = new byte[6 + sessionLength + contentBytes.Length];
            packet[0] = 2;
            packet[1] = (byte)sessionLength;
            Array.Copy(contentLengthBytes, 0, packet, 2, contentLengthBytes.Length);
            Array.Copy(sessionBytes, 0, packet, 6, sessionBytes.Length);
            Array.Copy(contentBytes, 0, packet, 6 + sessionBytes.Length, contentBytes.Length);
            return packet;
        }
        
        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {

            return true;
        }
    }


    class Client
    {
        private SslStream _SslStream;
        private const int BufferSize = 100;
        private int _LastIndex = 0;
        private byte[] _LastBuffer;
        public Client(SslStream stream)
        {
            this._SslStream = stream;
        }

        public void Write(byte[] data)
        {

            this.BeginWrite(data, 0, data.Length);
        }

        private void BeginWrite(byte[] data, int offset, int length)
        {
            if (length <= BufferSize)
            {
                this._SslStream.BeginWrite(data, offset, length, this.EndWrite, null);
                if (length < BufferSize)
                {
                    this._LastBuffer = null;
                }
            }
            else
            {
                this._LastBuffer = data;
                this._SslStream.BeginWrite(data, offset, BufferSize, this.EndWrite, null);
            }
        }

        private void EndWrite(IAsyncResult ar)
        {
            this._SslStream.EndWrite(ar);
            if (this._LastBuffer != null)
            {
                this._LastIndex += BufferSize;
                int len = this._LastBuffer.Length - this._LastIndex;
                int lenToBeWrite;
                if (len >= BufferSize)
                {
                    lenToBeWrite = BufferSize;
                }
                else
                {
                    lenToBeWrite = len;
                }
                this.BeginWrite(this._LastBuffer, this._LastIndex, lenToBeWrite);
            }
        }

    }





    class MySocket:Socket
    {
        public MySocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
            : base(addressFamily, socketType, protocolType)
        {

        }
        public MySocket(SocketInformation socketInformation) : base(socketInformation) { }
         new public  IAsyncResult BeginSend( byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, Object state)
        {
            return base.BeginSend(buffer, offset, size, socketFlags, callback, state);
        }

    }

    class MyNetworkStream : NetworkStream
    {
        private const int BUFFERSIZE = 100;
        private byte[] _Buf = new byte[200];
        private byte[] _ReadBuf = new byte[BUFFERSIZE];
        private byte[] _LastReadBuf;
        private int _LastReadOffset=0;
        private int _LastReadSize=0;
        public MyNetworkStream(Socket socket) : base(socket) { }
        public MyNetworkStream(Socket socket, bool ownsSocket) : base(socket, ownsSocket) { }
        public MyNetworkStream(Socket socket, FileAccess access) : base(socket, access) { }
        public MyNetworkStream(Socket socket, FileAccess access, bool ownsSocket) : base(socket, access, ownsSocket) { }
        [HostProtectionAttribute(SecurityAction.LinkDemand, ExternalThreading = true)]
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, Object state)
        {
            Buffer.BlockCopy(buffer, offset, _Buf, 0, size);
            return base.BeginWrite(_Buf, 0, size, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            base.EndWrite(asyncResult);
        }

        [HostProtectionAttribute(SecurityAction.LinkDemand, ExternalThreading = true)]
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, Object state)
        {
            this._LastReadBuf = buffer;
            this._LastReadOffset = offset;
            this._LastReadSize = size;
            return base.BeginRead(this._ReadBuf, offset, size, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            int len = base.EndRead(asyncResult);
            Array.Copy(this._ReadBuf, this._LastReadOffset, this._LastReadBuf, this._LastReadOffset, this._LastReadSize);
            return len;
        }
    }
}
