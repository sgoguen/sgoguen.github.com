open System

//  The Left Pipe operator.  Turns functions fluent
let (|>) x f = f(x)
let total = [1..10] |> List.sum

//  The Right Pipe operator.  Turns functions fluent
let (<|) f x = f(x)
let total = List.sum <| [1..10]

//  Function composition
let (>>) f g = fun x -> g(f(x))
let GetPage size page = Seq.skip (page * size) >> Seq.take size

//  Reverse function composition
let (<<) f g = fun x -> f(g(x))
let GetPage size page = Seq.take size << Seq.skip (page * size) 