open System.Net
open System.Net.Sockets
let localIPEndPoint = new IPEndPoint(IPAddress.Any,7777)
let client = new TcpClient(localIPEndPoint);
let remote = new IPEndPoint(IPAddress.Parse("202.83.220.36"),9000)
do
    client.Connect(remote)
