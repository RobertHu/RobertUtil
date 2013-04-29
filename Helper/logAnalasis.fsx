#load "common.fsx"
open System.IO
open Common
let dir = @"D:\Work\javaTrader\data3"
let files = seq{for i in 0..-1..0 do yield sprintf "mylogfile.txt.%d" i} |> Array.ofSeq
let target = "Login"

let pattern = ".*?(\[[0-9]+\].*?-).*"
let pat2 = ".*invokeId:(.*?),.*"

let allLines = 
    files
    |>Seq.map(fun f ->Path.Combine(dir,f))
    |>Seq.map(fun p -> File.ReadAllLines p)
    |>Seq.concat
    |>Array.ofSeq


let getInvokeIdWithLogin()=
    allLines
    |>Seq.filter(fun line -> line.LastIndexOf(target) <> -1)
    |>Seq.map(fun line -> 
        match pat2, line with
        |Reg result -> Some(result)
        |_  -> None
        )
    |>Seq.filter(fun m -> m.IsSome)


let getRecords (target: string) =
    allLines
    |>Seq.filter(fun line -> line.LastIndexOf(target) <> -1)
    |>Seq.iter(fun line ->
        match pattern ,line with
        |Reg result ->
            printfn "%s" (line.Replace(result,""))
        |_ -> ()
        )



let getLoginRecords() =
    getInvokeIdWithLogin()
    |>Seq.iteri(fun i m ->
        printfn "-----------------%d----------------------" i
        allLines
        |>Seq.filter(fun line -> line.LastIndexOf(m.Value) <> -1)
        |>Seq.iter(fun line ->
            match pattern , line with
            |Reg result ->
                printfn "%s" (line.Replace(result,""))
            |_ -> ()
            )
        printfn "----------------------------------------"
        )



getLoginRecords()