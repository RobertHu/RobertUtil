
let rec sum_integers a b =
    if a > b then 0
    else 
        a + sum_integers (a + 1) b

let cube a = a * a * a

let rec sum_cubes a b = 
    if a > b then 0
    else 
        (cube a) + (sum_cubes (a + 1) b)



let rec pi_sum (a : int) (b : int) =
    if a > b then 0.0
    else
        1.0 / float(a * (a + 2)) + (pi_sum (a + 4) b)

let rec sum term a next b =
    if a > b then 0.0
    else 
        (term a) + (sum term (next a) next b)

let inc a = a + 1

let sum_cube_new a b = sum cube a inc b

let identity a = a
let sum_new a b = sum identity a inc b

let pi_sum2 a b =
    let pi_term x = 1.0 / float(x * (x + 2))
    let pi_next x = x + 4
    sum pi_term a pi_next b
