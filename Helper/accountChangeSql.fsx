
open System.IO
let path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService.Common\LocalAccount.cs"
let splitSep = [|' '|]
let clasTemplate = "public class"
let propertyTemplate = "public virtual"
File.ReadAllLines(path)
|>Seq.filter(fun line -> line.Contains(clasTemplate) || line.Contains(propertyTemplate))
|>Seq.map(fun line -> line.Replace(clasTemplate,"").Replace(propertyTemplate,""))
|>Seq.map(fun line ->
    let index = line.LastIndexOf('{')
    match index with
    | -1 -> line.Trim()
    | _ -> line.Substring(0,index).Trim()
    )
|>Seq.map(fun line ->
    let fields = line.Split(splitSep)
    match fields.Length = 2 with
    |true -> (fields.[0],fields.[1])
    |false -> ("", fields.[0])
    )
|>Seq.map(fun (f1,f2) ->
    match f1 with
    |"" -> ("table",f2)
    |"Guid" |"int"|"string"  -> (f1,f2) 
    |"DateTime" -> (f1.ToLower(),f2)
    |_  -> ("int",f2)
    )

|>Seq.iter(fun line -> printfn "%A" line)