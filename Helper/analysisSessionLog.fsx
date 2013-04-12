open System.IO


let path = @"D:\Work\javaTrader\mylogfile.txt";

let data = seq{
        for line in File.ReadAllLines(path) do
            if line.Contains("Session+Monitor") then yield line
    }

data
|>Seq.iter(fun l -> printfn "%s" l)