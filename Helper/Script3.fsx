
#r "iExchange.Common.dll"
open System.Data
open iExchange.Common

let connstr= "data source=192.168.100.106;initial catalog=iExchange_V3;user id=sa;password=APP16899;Connect Timeout=30" 

let data = DataAccess.GetData(@"select * from dbo.Customer where Code like '%HU000%'",connstr)

let accountData = DataAccess.GetData(@"select * from dbo.Account where Code like '%HU000%'", connstr)


let qp_data = DataAccess.GetData("select ID from dbo.quotepolicy",connstr)

let tp_data = DataAccess.GetData("select ID from dbo.TradePolicy",connstr)

let codes = seq{
    for row in data.Tables.[0].Rows do
        yield string row.["Code"]
}


codes
|>Seq.iter(fun c -> printfn "%s" c)

let account_codes = seq{
    for row in accountData.Tables.[0].Rows do
        yield string row.["Code"]
}


let qp_ids = seq{
    for row in qp_data.Tables.[0].Rows do
        yield string row.["ID"]
}

let tp_ids = seq{
    for row in tp_data.Tables.[0].Rows do
        yield string row.["ID"]
}

let composit_ids = seq{
    for q_id in qp_ids do
        for t_id in tp_ids do
            yield q_id,t_id
}





let a_c_codes = Seq.zip codes account_codes
let length = (Array.ofSeq a_c_codes).Length


let result = Seq.zip a_c_codes (Seq.take length composit_ids)

let sql = seq{
    for (c_c, a_c), (q_i, t_i) in result do
        yield sprintf @"update dbo.Customer set PrivateQuotePolicyID = '%s' where Code = '%s'" q_i c_c , 1
        yield sprintf @"update dbo.Account set TradePolicyID = '%s' where Code = '%s'" t_i a_c , 0
}


//sql
//|>Seq.filter(fun (_ ,id) -> id = 0)
//|>Seq.iter(fun (s, _) -> printfn "%s" s)