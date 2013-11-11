#r "iExchange.Common.dll"
open iExchange.Common

let conn = "data source=testdb;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=30"

let data = DataAccess.ExecuteScalar("select TradeDayBeginTime from dbo.SystemParameter",conn)
printfn "%s" (data.ToString())

