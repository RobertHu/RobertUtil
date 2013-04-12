
open System
let price = "13/2:1.5330:1.5325:1.5342:1.5248::63988345::;4:1585.9:1585.0:1586.6:1570.3::63988345::;4:1588.4:1587.8:1589.1:1570.3::63988345::/"

let baseTime = new System.DateTime(2011,4,1,0,0,0)
let currentTime = baseTime.AddSeconds(63988345.0)

let start = price.IndexOf("/")
let last = price.LastIndexOf("/")
let command_sequence = price.Substring(0,start)

let quotation = price.Substring(start + 1, last - start - 1)

let qoes = quotation.Split([|';'|])

qoes
|> Seq.iter (fun s -> 
    let r = s.Split([|':'|])
    let id,ask,bid,high,low,prevclose,time,volumn,totalvolumn =
        r.[0],r.[1],r.[2],r.[3],r.[4],r.[5],baseTime.AddSeconds(System.Double.Parse(r.[6])),r.[7],r.[8]
    printfn "id=%s,ask=%s,bid=%s,high=%s,low=%s,prevclose=%s,time=%s,volumn=%s,totalvolumn=%s" id ask bid high low prevclose (time.ToString()) volumn totalvolumn
    
    )


