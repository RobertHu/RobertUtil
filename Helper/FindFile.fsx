open System
open System.IO;
open System.Text
let path = @""

let pattern = ".*?System.out.println.*?"

let rec getAllFiles (dirPath: string) =
    let files = Directory.GetFiles(dirPath)
    let dirs = Directory.GetDirectories(dirPath)
    seq{
        
    }


