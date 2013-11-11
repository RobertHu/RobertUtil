namespace FLib
module FH =
    type Class1() = 
        member this.X = "F#"

    let factorizeRecursize n =  
        let rec find i = 
            if  i > n then None
            elif (n % i = 0) then Some(i, n / i)
            else find(i+1)
        find(2)
            
