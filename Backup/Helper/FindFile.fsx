open System
open System.IO;
open System.Text
let path = @"D:\Work\javaTrader"

let pattern = ".*?System.out.print.*?"

let rec getAllFiles (dirPath: string) =
    let files = Directory.GetFiles(dirPath)
    let dirs = Directory.GetDirectories(dirPath)
    seq{
       yield! files
       for dir in dirs do
          yield! getAllFiles dir 
    }



let getMatchLine path pattern fileExtension =
    let regex = new RegularExpressions.Regex(pattern)
    let files = getAllFiles path
    files 
    |> Seq.filter (fun f -> Path.GetExtension(f) = fileExtension)
    |> Seq.map(fun f ->
            File.ReadAllLines(f)
            |>Seq.mapi(fun i line ->
                    match regex.IsMatch(line) with
                    |true -> (true, f, line,i + 1)
                    |false -> (false,null,null,i)
                )
            |>Seq.filter(fun (isFind, _ , _ , _ ) -> isFind)
        )
    |> Seq.concat
    |> Seq.iter(fun (_, f, line, num) ->
            printfn "[%s] [%d]  [%s]" f num line
        )
    


getMatchLine @"D:\GitDataSource\JavaTrader\Source" "System.console()" ".java"