
open System
open System.Text.RegularExpressions
open System.IO


let path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3Promotion\AsynchronousSslStream\Util\ClientRequestHelper.cs"


let (|Reg|_|) (pattern,input) =
    let regex = new Regex(pattern,RegexOptions.IgnoreCase)
    let result = regex.Match(input)
    if result.Success then Some(result.Groups.[1].Value) else None


let input ="if (methodName == \"GetInitData\")"
let pattern = "if\s*\(\s*methodName\s*==\s*(.*)\s*\)"



let getAllMethodActions pattern =
    seq{
        for line in File.ReadAllLines(path) do
            match pattern, line with
            |Reg result -> 
                let r = result.Replace("\"","")
                yield (r,r + "Action")
            |_ -> ()
    }


let produceMethodSignature() =
    getAllMethodActions pattern
    |>Seq.iter(fun (_, s2) -> printfn "private XmlNode %s(SerializedObject request, Token token)\n" s2)


let produceAddContent() =
    getAllMethodActions pattern
    |>Seq.iter (fun (s1,s2) -> printfn "table.Add(\"%s\",%s);" s1 s2)

produceAddContent()
   