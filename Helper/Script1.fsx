#r "System.Xml.Linq"
open System
open System.Xml
open System.Xml.Linq


let input = "<Request><Arguments><LoginID>BCC009</LoginID><Password>12345678</Password><Version></Version><appType>7</appType></Arguments><Method>Login</Method><InvokeId>56e1a265-7794-4616-83ee-2fcb87f1c605</InvokeId></Request>"

let xmlTree = XElement.Parse("<Root> <Child> </Child> </Root>");
let xml = XElement.Parse(input)
printfn "%s" (xmlTree.ToString())
