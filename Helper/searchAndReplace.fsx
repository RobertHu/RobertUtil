
open System.IO
open System.Text.RegularExpressions

let filePath = @"D:\Work\javaTrader\setting.txt"

let newFilePath = @"D:\Work\javaTrader\lines.txt"

printfn "%s" newFilePath

let pattern = ".*?(xmlNode2.get_Item\(\".*\"\).get_InnerText\(\)).*"

let pattern2 = ".*?(xmlNode2.get_Item\(\".*\"\)).*"

let innerPattern = ".*?(\".*\").*"

let regex = new Regex(pattern)
let regex2= new Regex(pattern2)

let innerRegex = new Regex(innerPattern)


let lines = File.ReadAllLines(filePath)
let fileStream = new FileStream(newFilePath,FileMode.Create,FileAccess.Write)
let fileWriter= new StreamWriter(fileStream)

let replaceFun (x: Match) (origin: string) (search: string) isOutter =
    let vv = x.Groups.[1].Value.Replace("\"","");
    let replaceStr = if isOutter then sprintf "settingElement.getFirstChildElement(\"%s\")" vv else sprintf "XmlElementHelper.getInnerValue(settingElement,\"%s\")" vv
    let ss = origin.Replace(search,replaceStr)
    ss

let replaceHelper (m: Match) (origin: string) isOutter =
     let search = m.Groups.[1].Value
     let mm = innerRegex.Match(search)
     match mm.Success with
     |true -> replaceFun mm origin search isOutter
     |false -> origin



lines
|>Seq.map(fun line ->
    let m = regex.Match(line)
    match m.Success with
    |true -> replaceHelper m line false
    |false ->
        let match2 = regex2.Match(line)
        match match2.Success with
        |true -> replaceHelper match2 line true
        |false -> line
           
)
|>Seq.iter(fun line ->
       fileWriter.WriteLine(line) 
    )
fileWriter.Close()


