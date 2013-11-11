#r "System.Web.dll"
open System.Web
open System.Text
let input = "<News C_S=\"40\"><NewsItem Id=\"8b8499ee-24d6-46b0-9f5c-5fd7026e5424\" PublishTime=\"2013-06-04 11:53:37\" Language=\"Chs\" Title=\"11:53 港股 西王特钢&lt;1266&gt; 月报表\" /><NewsItem Id=\"837d4948-0bbe-4a84-a5c6-801c13feb59c\" PublishTime=\"2013-06-04 11:53:30\" Language=\"Chs\" Title=\"11:53 港股 恒指花旗四一牛A&lt;69236&gt; 债券及结构性产品,牛熊证到期公告\" /></News>"
let str = HttpUtility.HtmlDecode(input)
let bytes = Encoding.UTF8.GetBytes(str)
printfn "%s" str

let newStr = Encoding.UTF8.GetString(bytes)
printfn "%s" newStr

