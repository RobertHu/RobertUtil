
#r "Newtonsoft.Json.dll"

open System.Net
open System.Net.Security
open System.Net.Sockets
open Newtonsoft.Json.Linq

let ip = "127.0.0.1"
let port = 8888

   
let tcpClient = new TcpClient(ip,port)

let sslStream = new SslStream(tcpClient.GetStream(),false,new RemoteCertificateValidationCallback(fun _ _ _ _  -> true),null)

    



