open System.Text.RegularExpressions
let (|Reg|_|) (pattern,input) =
    let regex = new Regex(pattern,RegexOptions.IgnoreCase)
    let result = regex.Match(input)
    if result.Success then Some(result.Groups.[1].Value) else None
