#r "System.Xml.Linq.dll"
open System.Xml
open System.Xml.Linq


let x = XElement.Parse("<Request><Arguments><argElment>test10</argElment><argElment>12345678</argElment><argElment>1.6.0; Sun Microsystems Inc.; Windows 2003; 1; 63m; CHS</argElment><argElment>7</argElment></Arguments><Method>Login</Method><InvokeId>c2e85b36-487e-4da3-a49c-3e8ef21db124</InvokeId></Request>");
printfn "%s" (x.ToString())

