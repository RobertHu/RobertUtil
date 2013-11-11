
#r "System.Xml.Linq.dll"
open System.Xml
open System.Xml.Linq

let filePath= @"D:\VsProjects\RobertUtil\FaxEmail_Fsharp\data.xml"


let root = XElement.Load(filePath)

let newAccount=root.Element(XName.op_Implicit("NewAccount"))
printf "%A" newAccount

let x = "".Split(';')
match x = null with
|true -> printf "null \n"
|false -> printf "false \n"
printf "%A\n" x



