

open System



let constStr = "huhuabo"

let convertToBase64 (str : string) =
    let bytes = System.Text.Encoding.ASCII.GetBytes(str)
    let result = Convert.ToBase64String(bytes)
    result


let fromBase64 (str : string) = 
    let bytes = Convert.FromBase64String(str)
    let result = System.Text.Encoding.ASCII.GetString(bytes)
    result