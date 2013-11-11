
let sum n m =
    let mutable res = 0
    for i = n to m do
        res <- res + i
    res


open System.Collections.Generic

let divideIntoEquivalenceClasses keyf seq =
    let dict= new Dictionary<'key,ResizeArray<'T>>()
    seq |> Seq.iter(fun v ->
         let key = keyf v
         let ok, prev = dict.TryGetValue(key)
         if ok then prev.Add(v)
         else let prev = new ResizeArray<'T>()
              dict.[key] <- prev
              prev.Add(v)
        )
    dict |> Seq.map(fun group -> group.Key, Seq.readonly group.Value)





type Tree<'T> =
    |Tree  of  'T * Tree<'T> * Tree<'T>
    |Tip of 'T


let rec sizeOfTree tree  =
    match tree with
    |Tree(_,l,r) -> 1 + sizeOfTree(l) + sizeOfTree(r)
    |Tip _ -> 1
