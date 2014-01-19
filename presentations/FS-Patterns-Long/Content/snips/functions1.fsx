open System

// [snippet:These are all the same function]
let add x y = x + y
let add = fun x y -> x + y
let add = fun x -> fun y -> x + y
// [/snippet]

// [snippet:You can pass parameters one by one]
let add5 = add 5
let x = add5 3  //  x should be 8
// [/snippet]