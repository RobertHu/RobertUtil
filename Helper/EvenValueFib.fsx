

open System.Collections.Generic

let rec  fib n  =
    match n with
    |1 -> 1
    |2 -> 2
    |_  -> fib(n-1) + fib(n-2)


let source = seq{for i in 1..10 do yield i}

source
|>Seq.map(fun i -> fib i)
|>Seq.iter(fun i -> printfn "%d" i)

    