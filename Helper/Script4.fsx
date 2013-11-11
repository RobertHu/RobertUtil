open System.Diagnostics

let mutable i = 0
let name = "iexplore"
while i < 1000 do 
    Process.Start("http://item.taobao.com/item.htm?spm=a1z10.1.w4004-1570239210.1.xmUXiO&id=18431574879") |> ignore
    System.Threading.Thread.Sleep(5000)
    System.Diagnostics.Process.GetProcessesByName(name)
    |>Seq.iter(fun p ->
        try
            p.Kill()
            printfn("killed")
        with
        |x -> ()
        )
    printfn("ok")
    i <- i + 1
