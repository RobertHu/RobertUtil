

let template1 = """
        [ConfigurationProperty("$1",IsRequired=true)]
        public string $2
        {
            get { return this["$1"] as string; }
        }
"""

let template2 = """
        [ConfigurationProperty("$1")]
        public ServiceCollection $1
        {
            get
            {
                return (ServiceCollection)this["$1"] ?? new ServiceCollection();
            }
        }
"""

let template3 = """
        private NameValueCollection _$1;
        public NameValueCollection $1
        {
            get
            {
                if (_$1 == null)
                {
                    lock (_Lock)
                    {
                        if (_$1 == null)
                        {
                            _$1 = FillSettins(ServiceSettings.GetConfig().$1);
                        }
                    }
                }
                return _$1;
            }
        }
"""

open System

let getTarget (template:string) (input:string) =
    let result = new System.Text.StringBuilder()
    let chars = template.ToCharArray()
    let len = chars.Length
    let mutable i = 0
    while(i < len)  do
        match  chars.[i] with
        |'$' -> 
            i <- i + 1
            match chars.[i] with
            |'1' -> result.Append(input)|>ignore
            |'2' -> 
                let myInput = input.ToCharArray()
                myInput.[0] <- System.Char.ToUpper(myInput.[0])
                let str = new System.String(myInput)
                result.Append(str) |> ignore
            |_ -> ()
        | c -> result.Append(c) |> ignore
        i <- i + 1
    result


#r "System.Xml.Linq.dll"
open System.Xml
open System.Xml.Linq

let source ="""
<items>
<add key="iExchange.StateServer.Service" value="http://10.2.6.1/iExchangeCollection/iExchange3%20Team/iExchange3/StateServer/Service.asmx" />
<add key="SecurityServiceUrl" value="http://10.2.6.1/iExchangeCollection/iExchange3%20Team/iExchange3/Security/Web/Service/securityservices.asmx" />
<add key="ParticipantServiceUrl" value="http://10.2.6.1/iExchangeCollection/iExchange3%20Team/iExchange3/Security/Web/Service/participantservices.asmx" />
<add key="backofficeServiceUrl" value="http://10.2.6.1/iExchangeCollection/iExchange3%20Team/iExchange3/backoffice/webservice/ServiceSettings.asmx" />
</items>
"""

let parseXML() =
    let toName = XName.op_Implicit
    let getAttr (ele:XElement) name = ele.Attribute(toName name).Value
    let root = XElement.Parse(source)
    root.Elements(toName "add")
    |>Seq.map(fun e -> (getAttr e "key", getAttr e "value"))
    |>Seq.map(fun (k, v) -> sprintf """<add key="%s" value="%s" />""" k v)
    |>Seq.iter(fun s -> printfn "%s" s)



        
        
   
